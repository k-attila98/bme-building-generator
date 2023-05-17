﻿using BuildingGenerator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Vector3 = BuildingGenerator.Shared.Vector3;

namespace BuildingGenerator.BuildingGenerator
{
    public class BuildingPrefabPlacer
    {
        public Transform floorPrefab;
        public Transform[] wallPrefab;
        public Transform[] roofPrefab;
        Transform bldgFolder;
        private float wallHeight = 4f;
        private float wallWidth = 2f;

        private List<Transform> placedPrefabs = new List<Transform>();
        private Dictionary<int, Story[]> storiesByLevel = new Dictionary<int, Story[]>();

        private HashSet<string> placedFloorPositions = new HashSet<string>();
        private HashSet<string> placedRoofPositions = new HashSet<string>();

        public List<Transform> PlacePrefabs(Building bldg)
        {
            if (wallPrefab.Length == 0 || roofPrefab.Length == 0 || floorPrefab == null)
            {
                Console.Write("Component [BuildingSerializer] doesn't have required prefabs set!");
                throw new NullReferenceException("Component [BuildingSerializer] doesn't have required prefabs set!");
            }

            bldgFolder = new Transform("Building");

            wallWidth = wallPrefab[0].Width;
            wallHeight = wallPrefab[0].Height;

            _SetStoriesByLevel(bldg);

            foreach (Wing wing in bldg.Wings)
            {
                _RenderWing(wing);
            }

            Transform roofFolder = new Transform("Roof");
            roofFolder.SetParent(bldgFolder);
            foreach (var wing in bldg.Wings)
            {
                _RenderRoofToEveryDynamicSizedStory(wing, roofFolder);
            }

            return placedPrefabs;
        }

        private void _SetStoriesByLevel(Building bldg)
        {
            foreach (Wing wing in bldg.Wings)
            {
                foreach (Story story in wing.Stories)
                {
                    if (!storiesByLevel.ContainsKey(story.Level))
                    {
                        storiesByLevel.Add(story.Level, new Story[] { story });
                    }
                    else
                    {
                        Story[] stories = storiesByLevel[story.Level];
                        storiesByLevel[story.Level] = new Story[stories.Length + 1];
                        stories.CopyTo(storiesByLevel[story.Level], 0);
                        storiesByLevel[story.Level][stories.Length] = story;
                    }
                }
            }
        }

        private RectInt[] _GetIntersectionsOnLevelForStory(int level, Story story)
        {
            var stories = storiesByLevel[level];
            List<RectInt> intersections = new List<RectInt>();

            // on the given level we should filter out the story which we are generating, or it will intersect with itself
            var otherStories = stories.Except(stories.Where(s => s.Bounds == story.Bounds));

            foreach (var otherStory in otherStories)
            {
                intersections.Add(story.Bounds.Intersect(otherStory.Bounds));
            }

            return intersections.ToArray();
        }

        private void _RenderWing(Wing wing)
        {
            Transform wingFolder = new Transform("Wing");
            wingFolder.SetParent(bldgFolder);
            foreach (Story story in wing.Stories)
            {
                _RenderStory(story, wing, wingFolder);
            }

        }

        private bool _IsWallClipping(int x, int y, RectInt[] intersectionsOnLevel)
        {
            if (intersectionsOnLevel == null || intersectionsOnLevel.Length < 1)
            {
                return false;
            }

            foreach (RectInt intersection in intersectionsOnLevel)
            {
                if (intersection.Contains(x, y))
                {
                    return true;
                }
            }

            return false;
        }

        private void _RenderStory(Story story, Wing wing, Transform wingFolder)
        {
            Transform storyFolder = new Transform("Story " + story.Level);
            storyFolder.SetParent(wingFolder);

            RectInt[] intersections = _GetIntersectionsOnLevelForStory(story.Level, story);

            for (int x = story.Bounds.min.x; x < story.Bounds.max.x; x++)
            {
                for (int y = story.Bounds.min.y; y < story.Bounds.max.y; y++)
                {

                    _PlaceFloor(x, y, story.Level, storyFolder);

                    if (_IsWallClipping(x, y, intersections))
                    {
                        continue;
                    }

                    //south wall
                    if (y == story.Bounds.min.y)
                    {
                        Transform wall = wallPrefab[(int)story.Walls[x - story.Bounds.min.x]];
                        _PlaceSouthWall(x, y, story.Level, storyFolder, wall);
                    }

                    //east wall
                    if (x == story.Bounds.min.x + story.Bounds.size.x - 1)
                    {
                        Transform wall = wallPrefab[(int)story.Walls[story.Bounds.size.x + y - story.Bounds.min.y]];
                        _PlaceEastWall(x, y, story.Level, storyFolder, wall);
                    }

                    //north wall
                    if (y == story.Bounds.min.y + story.Bounds.size.y - 1)
                    {
                        Transform wall = wallPrefab[(int)story.Walls[story.Bounds.size.x * 2 + story.Bounds.size.y - (x - story.Bounds.min.x + 1)]];
                        _PlaceNorthWall(x, y, story.Level, storyFolder, wall);
                    }

                    //west wall
                    if (x == story.Bounds.min.x)
                    {
                        Transform wall = wallPrefab[(int)story.Walls[(story.Bounds.size.x + story.Bounds.size.y) * 2 - (y - story.Bounds.min.y + 1)]];
                        _PlaceWestWall(x, y, story.Level, storyFolder, wall);
                    }

                }
            }
        }

        private void _PlaceFloor(int x, int y, int level, Transform storyFolder)
        {
            var transformPoint = new Vector3(
                    x * floorPrefab.Width,
                    0f + level * wallHeight,
                    y * floorPrefab.Width
                );

            if (placedFloorPositions.Contains(transformPoint.ToString()))
            {
                return;
            }

            Transform f = new Transform(floorPrefab, transformPoint, Quaternion.Identity);
            f.SetParent(storyFolder);

            placedFloorPositions.Add(f.Position.ToString());
            placedPrefabs.Add(f);
        }

        private void _PlaceSouthWall(int x, int y, int level, Transform storyFolder, Transform wall)
        {
            Transform w = new Transform(
                wall,
                storyFolder.TransformPoint(
                        new Vector3(
                        x * wallWidth,
                        level * wallHeight,
                        y * wallWidth
                        )
                    ),
                Quaternion.Identity
            );
            w.Name = "south wall";
            w.SetParent(storyFolder);

            placedPrefabs.Add(w);
        }

        private void _PlaceEastWall(int x, int y, int level, Transform storyFolder, Transform wall)
        {
            Transform w = new Transform(
                wall,
                storyFolder.TransformPoint(
                    new Vector3(
                        x * wallWidth + wallWidth,
                        level * wallHeight,
                        y * wallWidth + wallWidth
                        )
                    ),
                Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitY, MathHelper.GetRadFromDeg(90))
            );
            w.Name = "east wall";
            w.SetParent(storyFolder);

            placedPrefabs.Add(w);
        }

        private void _PlaceNorthWall(int x, int y, int level, Transform storyFolder, Transform wall)
        {
            Transform w = new Transform(
                wall,
                storyFolder.TransformPoint(
                    new Vector3(
                        x * wallWidth + wallWidth,
                        level * wallHeight,
                        y * wallWidth + wallWidth
                        )
                    ),
                Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitY, MathHelper.GetRadFromDeg(180))
            );
            w.Name = "north wall";
            w.SetParent(storyFolder);

            placedPrefabs.Add(w);
        }

        private void _PlaceWestWall(int x, int y, int level, Transform storyFolder, Transform wall)
        {
            Transform w = new Transform(
                wall,
                storyFolder.TransformPoint(
                    new Vector3(
                        x * wallWidth,
                        level * wallHeight,
                        y * wallWidth
                        )
                    ),
                Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitY, MathHelper.GetRadFromDeg(-90))
            );
            w.Name = "west wall";
            w.SetParent(storyFolder);

            placedPrefabs.Add(w);
        }

        private void _RenderRoofOnTop(Wing wing, Transform wingFolder)
        {
            for (int x = wing.Bounds.min.x; x < wing.Bounds.max.x; x++)
            {
                for (int y = wing.Bounds.min.y; y < wing.Bounds.max.y; y++)
                {
                    _PlaceRoof(x, y, wing.Stories.Length, wing, wingFolder);
                }
            }
        }

        private void _RenderRoofOnTopWithDynamicSize(Wing wing, Transform wingFolder)
        {
            for (int x = wing.Stories[wing.Stories.Length - 1].Bounds.min.x; x < wing.Stories[wing.Stories.Length - 1].Bounds.max.x; x++)
            {
                for (int y = wing.Stories[wing.Stories.Length - 1].Bounds.min.y; y < wing.Stories[wing.Stories.Length - 1].Bounds.max.y; y++)
                {
                    _PlaceRoof(x, y, wing.Stories.Length, wing, wingFolder);
                }
            }
        }

        private void _RenderRoofToEveryDynamicSizedStory(Wing wing, Transform wingFolder)
        {

            if (wing.Stories.Length == 1)
            {
                _RenderRoofOnTop(wing, wingFolder);
                return;
            }

            var storiesThatNeedRoof = new List<Story>();
            for (int i = 0; i < wing.Stories.Length - 1; i++)
            {
                var lowerStory = wing.Stories[i];
                var higherStory = wing.Stories[i + 1];

                if (!lowerStory.Bounds.Equals(higherStory.Bounds))
                {
                    storiesThatNeedRoof.Add(lowerStory);
                }
            }

            if (wing.GetRoof.Type == RoofType.ProceduralPeak)
            {
                foreach (var story in storiesThatNeedRoof)
                {
                    var roofLevel = story.Level + 1;
                    if (roofLevel > 0 && wing.Stories.Length > roofLevel)
                    {
                        var dividedBounds = story.Bounds.SubtractAndDivide(wing.Stories[roofLevel].Bounds);
                        foreach (var bounds in dividedBounds)
                        {
                            _PlaceScaledRoofOnBounds(roofLevel, bounds, wing, wingFolder);
                        }
                    }
                    /*
                    else if (wing.Stories.Length == roofLevel)
                    {
                        var roofBounds = wing.Stories[story.Level].Bounds;
                        _PlaceScaledRoofOnBounds(wing.Stories.Length, roofBounds, wing, wingFolder);
                    }
                    */
                }
                var roofBounds = wing.Stories[wing.Stories.Length-1].Bounds;
                _PlaceScaledRoofOnBounds(wing.Stories.Length, roofBounds, wing, wingFolder);
            }
            else
            {
                foreach (var story in storiesThatNeedRoof)
                {
                    var roofLevel = story.Level + 1;
                    for (int x = story.Bounds.min.x; x < story.Bounds.max.x; x++)
                    {
                        for (int y = story.Bounds.min.y; y < story.Bounds.max.y; y++)
                        {
                            _PlaceRoof(x, y, roofLevel, wing, wingFolder);
                        }
                    }
                }

                _RenderRoofOnTopWithDynamicSize(wing, wingFolder);

            }

            
        }

        private void _PlaceScaledRoofOnBounds(int level, RectInt roofBounds, Wing wing, Transform wingFolder)
        {
            RoofDirection direction = wing.GetRoof.Direction;
            RoofType type = wing.GetRoof.Type;
            float roofWidth = roofPrefab[(int)type % roofPrefab.Length].Width;

            var transformPoint = wingFolder.TransformPoint(
                    new Vector3(
                            roofBounds.xMin * roofWidth,
                            level * wallHeight,
                            roofBounds.yMin * roofWidth
                        )
                    );

            var prefabToBeUsed = roofPrefab[(int)type % roofPrefab.Length].Clone();

            prefabToBeUsed.Scale(new Vector3(roofBounds.width, Math.Max(1, roofBounds.width * roofBounds.height / 6), roofBounds.height));

            Transform r;
            r = new Transform(
                prefabToBeUsed,
                transformPoint,
                //Quaternion.Euler(0f, rotationOffset[(int)direction].y, 0f)
                Quaternion.Identity
                );
            r.SetParent(wingFolder);


            for (int a = roofBounds.min.x; a < roofBounds.max.x; a++)
            {
                for (int b = roofBounds.min.y; b < roofBounds.max.y; b++)
                {
                    placedRoofPositions.Add(new Vector3(a * roofWidth, r.Position.y, b * roofWidth).ToString());
                }
            }

            placedPrefabs.Add(r);

        }

        private void _PlaceRoof(int x, int y, int level, Wing wing, Transform wingFolder)
        {
            //int level = wing.Stories.Length;
            RoofType type = wing.GetRoof.Type;
            RoofDirection direction = wing.GetRoof.Direction;
            float roofWidth = roofPrefab[(int)type % roofPrefab.Length].Width;

            var transformPoint = wingFolder.TransformPoint(
                    new Vector3(
                            x * roofWidth,
                            level * wallHeight,
                            y * roofWidth
                        )
                    );

            if (
                placedFloorPositions.Contains(transformPoint.ToString())
                || placedRoofPositions.Contains(transformPoint.ToString())
               )
            {
                return;
            }

            var prefabToBeUsed = roofPrefab[(int)type % roofPrefab.Length].Clone();

            Transform r;
            r = new Transform(
                prefabToBeUsed,
                transformPoint,
                //Quaternion.Euler(0f, rotationOffset[(int)direction].y, 0f)
                Quaternion.Identity
                );
            r.SetParent(wingFolder);

            placedRoofPositions.Add(r.Position.ToString());

            placedPrefabs.Add(r);
        }

        /**
         * Now unused, there is no unsymmetric prefab in the prefabs folder
         */
        Vector3[] rotationOffset = {
        new Vector3 (-0f, 270f, 0f),
        new Vector3 (0f, 0f, 0f),
        new Vector3 (0f, 90, -0f),
        new Vector3 (-0f, 180, -0f)
    };
    }
}
