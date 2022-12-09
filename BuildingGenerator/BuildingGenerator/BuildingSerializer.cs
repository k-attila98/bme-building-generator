using BuildingGenerator.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Vector3 = BuildingGenerator.Shared.Vector3;

public class BuildingSerializer
{
    
    public Transform floorPrefab;
    public Transform[] wallPrefab;
    public Transform[] roofPrefab;
    Transform bldgFolder;
    private float wallHeight = 4f;
    private float wallWidth = 2f;

    private List<Transform> placedPrefabs = new List<Transform>();
    private Dictionary<int, Story[]> storiesByLevel = new Dictionary<int, Story[]>();

    private HashSet<string> placedFloorsPositions = new HashSet<string>();

    public void SerializeToObj(Building bldg)
    {
        if (wallPrefab.Length == 0 || roofPrefab.Length == 0 || floorPrefab == null)
        {
            Console.Write("Component [BuildingSerializer] doesn't have required prefabs set!");
            return;
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
        
        _SaveBuilding();
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
        Transform f = new Transform(floorPrefab, new Vector3(
                x * floorPrefab.Width,
                0f + level * wallHeight,
                y * floorPrefab.Width
            ), Quaternion.Identity);
        f.SetParent(storyFolder);

        placedFloorsPositions.Add(f.Position.ToString());
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
                _PlaceRoof(x, y, wing.Stories.Length, wingFolder, wing.GetRoof.Type, wing.GetRoof.Direction);
            }
        }
    }
    
    private void _RenderRoofOnTopWithDynamicSize(Wing wing, Transform wingFolder)
    {
        for (int x = wing.Stories[wing.Stories.Length-1].Bounds.min.x; x < wing.Stories[wing.Stories.Length-1].Bounds.max.x; x++)
        {
            for (int y = wing.Stories[wing.Stories.Length-1].Bounds.min.y; y < wing.Stories[wing.Stories.Length-1].Bounds.max.y; y++)
            {
                _PlaceRoof(x, y, wing.Stories.Length, wingFolder, wing.GetRoof.Type, wing.GetRoof.Direction);
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
        for (int i = 0; i < wing.Stories.Length-1; i++)
        {
            var lowerStory = wing.Stories[i];
            var higherStory = wing.Stories[i + 1];

            if (!lowerStory.Bounds.Equals(higherStory.Bounds))
            {
                storiesThatNeedRoof.Add(lowerStory);
            }
        }
        /*
        var storiesArray = storiesThatNeedRoof.ToArray();
        for (int i = 0; i < storiesArray.Length; i++)
        {
            var story = storiesArray[i];
            for (int x = story.Bounds.min.x; x < story.Bounds.max.x; x++)
            {
                for (int y = story.Bounds.min.y; y < story.Bounds.max.y; y++)
                {
                    PlaceRoof(x, y, story.Level + 1, wingFolder, wing.GetRoof.Type, wing.GetRoof.Direction);
                }
            }
        }
        */
        
        foreach (var story in storiesThatNeedRoof)
        {
            for (int x = story.Bounds.min.x; x < story.Bounds.max.x; x++)
            {
                for (int y = story.Bounds.min.y; y < story.Bounds.max.y; y++)
                {
                    _PlaceRoof(x, y, story.Level + 1, wingFolder, wing.GetRoof.Type, wing.GetRoof.Direction);
                }
            }
        }
        
        _RenderRoofOnTopWithDynamicSize(wing, wingFolder);
    }

    private void _PlaceRoof(int x, int y, int level, Transform wingFolder, RoofType type, RoofDirection direction)
    {
        var transformPoint = wingFolder.TransformPoint(
                new Vector3(
                        x * roofPrefab[(int)type].Width,
                        level * wallHeight, //+ (type == RoofType.Point ? -0.3f : 0f),
                        y * roofPrefab[(int)type].Width
                    )
                );

        if (placedFloorsPositions.Contains(transformPoint.ToString()))
        {
            return;
        }

        Transform r;
        r = new Transform(
            roofPrefab[(int)type],
            transformPoint,
            //Quaternion.Euler(0f, rotationOffset[(int)direction].y, 0f)
            Quaternion.Identity
            );
        r.SetParent(wingFolder);
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

    private void _SaveBuilding()
    {
        Console.WriteLine("Serializing building...");
        using (StreamWriter writer = new StreamWriter(File.Open($"../../../Generated/building-{DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss")}.obj", System.IO.FileMode.Create)))
        {
            
            foreach (var prefab in placedPrefabs)
            {
                writer.Write(prefab.VerticesToString());
                Console.WriteLine("Serialized " + prefab.Name + "\n");
            }

            foreach (var prefab in placedPrefabs)
            {
                writer.Write(prefab.FacesToString());
            }
            
            Console.WriteLine("Serialization complete!");
        }
    }

    /**
     * Mainly for testing purposes, if we want to see the generated parts step by step 
     */
    private void _SaveBuilding(int num)
    {
        Console.WriteLine("Serializing building...");
        using (StreamWriter writer = new StreamWriter(File.Open($"../../../Generated/building-{DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss")}-{num}.obj", System.IO.FileMode.Create)))
        {

            foreach (var prefab in placedPrefabs)
            {
                writer.Write(prefab.VerticesToString());
                Console.WriteLine("Serialized " + prefab.Name + "\n");
            }

            foreach (var prefab in placedPrefabs)
            {
                writer.Write(prefab.FacesToString());
            }

            Console.WriteLine("Serialization complete!");
        }
    }

    /**
     * Mainly for testing purposes
     */
    public string StringifyBuilding()
    {
        string objFileContent = "";
        Console.WriteLine("Serializing building...");
        foreach (var prefab in placedPrefabs)
        {
            objFileContent += prefab.VerticesToString();
            Console.WriteLine("Serialized " + prefab.Name + "\n");
        }

        foreach (var prefab in placedPrefabs)
        {
            objFileContent += prefab.FacesToString();
        }
        Console.WriteLine("Serialization complete!");
        return objFileContent;
    }
}