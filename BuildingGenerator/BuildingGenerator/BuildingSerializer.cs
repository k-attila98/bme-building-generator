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

    public void SerializeToObj(Building bldg)
    {
        if (wallPrefab.Length == 0 || roofPrefab.Length == 0 || floorPrefab == null)
        {
            Console.Write("Component [BuildingRenderer] doesn't have required prefabs set!");
            return;
        }
        
        bldgFolder = new Transform("Building");
        
        wallWidth = wallPrefab[0].Width;
        wallHeight = wallPrefab[0].Height;
        
        foreach (Wing wing in bldg.Wings) 
        {
            RenderWing(wing);
        }
    }

    private void RenderWing(Wing wing)
    {
        Transform wingFolder = new Transform("Wing");
        wingFolder.SetParent(bldgFolder);
        foreach (Story story in wing.Stories) 
        {
            RenderStory(story, wing, wingFolder);
        }
        RenderRoofToEveryDynamicSizedStory(wing, wingFolder);
    }

    // TODO: ezt a függvényt befejezni, jelenleg nincs ötletem hogyan lehet elegánsan megoldani
    private void RemoveWallClippings(Wing[] wings)
    {
        if (wings.Length < 2)
        {
            return;
        }
        
        for (int i = 0; i < wings.Length-1; i++)
        {
            var wing1 = wings[i];
            var wing2 = wings[i + 1];
            int storyCount = 0;
            if (wing1.Stories.Length <= wing2.Stories.Length)
            {
                storyCount = wing1.Stories.Length-1;
            }
            else
            {
                storyCount = wing2.Stories.Length-1;
            }

            for (int j = 0; j < storyCount; j++)
            {
                if (wing1.Stories[j].Bounds.Overlaps(wing2.Stories[j].Bounds))
                {
                    // TODO: itt kiszámolni hogy hol lapolódik át és eltüntetni azokat a falakat
                }
            }
        }
    }

    private void RenderStory(Story story, Wing wing, Transform wingFolder)
    {
        Transform storyFolder = new Transform("Story " + story.Level);
        storyFolder.SetParent(wingFolder);
        //for (int x = wing.Bounds.min.x; x < wing.Bounds.max.x; x++)
        for (int x = story.Bounds.min.x; x < story.Bounds.max.x; x++)
        {
            //for (int y = wing.Bounds.min.y; y < wing.Bounds.max.y; y++)
            for (int y = story.Bounds.min.y; y < story.Bounds.max.y; y++)
            {
                PlaceFloor(x, y, story.Level, storyFolder);

                /*
                //south wall
                if (y == wing.Bounds.min.y) 
                {
                    Transform wall = wallPrefab[(int)story.Walls[x - wing.Bounds.min.x]];
                    PlaceSouthWall(x, y, story.Level, storyFolder, wall);
                }

                //east wall
                if (x == wing.Bounds.min.x + wing.Bounds.size.x - 1) 
                {
                    Transform wall = wallPrefab[(int)story.Walls[wing.Bounds.size.x + y - wing.Bounds.min.y]];
                    PlaceEastWall(x, y, story.Level, storyFolder, wall);
                }

                //north wall
                if (y == wing.Bounds.min.y + wing.Bounds.size.y - 1)
                {
                    Transform wall = wallPrefab[(int)story.Walls[wing.Bounds.size.x * 2 + wing.Bounds.size.y - (x - wing.Bounds.min.x + 1)]];
                    PlaceNorthWall(x, y, story.Level, storyFolder, wall);
                }

                //west wall
                if (x == wing.Bounds.min.x)
                {
                    Transform wall = wallPrefab[(int)story.Walls[(wing.Bounds.size.x + wing.Bounds.size.y) * 2 - (y - wing.Bounds.min.y + 1)]];
                    PlaceWestWall(x, y, story.Level, storyFolder, wall);
                }
                */
                if (x == story.Bounds.min.x) 
                {
                    Transform wall = wallPrefab[(int)story.Walls[x - story.Bounds.min.x]];
                    PlaceSouthWall(x, y, story.Level, storyFolder, wall);
                }

                //east wall
                if (y == story.Bounds.min.y + story.Bounds.size.y - 1) 
                {
                    Transform wall = wallPrefab[(int)story.Walls[story.Bounds.size.x + y - story.Bounds.min.y]];
                    PlaceEastWall(x, y, story.Level, storyFolder, wall);
                }

                //north wall
                if (x == story.Bounds.min.x + story.Bounds.size.x - 1)
                {
                    Transform wall = wallPrefab[(int)story.Walls[story.Bounds.size.x * 2 + story.Bounds.size.y - (x - story.Bounds.min.x + 1)]];
                    PlaceNorthWall(x, y, story.Level, storyFolder, wall);
                }

                //west wall
                if (y == story.Bounds.min.y)
                {
                    Transform wall = wallPrefab[(int)story.Walls[(story.Bounds.size.x + story.Bounds.size.y) * 2 - (y - story.Bounds.min.y + 1)]];
                    PlaceWestWall(x, y, story.Level, storyFolder, wall);
                }

            }
        }
    }

    private void PlaceFloor(int x, int y, int level, Transform storyFolder)
    {
        //Transform f = Instantiate(floorPrefab, storyFolder.TransformPoint(new Vector3(x * -wallWidth, 0f + level * wallHeight, y * -wallWidth)), Quaternion.identity);
        Transform f = new Transform(floorPrefab, new Vector3(
                x * wallWidth,
                0f + level * wallHeight,
                y * wallWidth
            ), Quaternion.Identity);
        f.SetParent(storyFolder);

        placedPrefabs.Add(f);
    }

    private void PlaceSouthWall(int x, int y, int level, Transform storyFolder, Transform wall)
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

    private void PlaceEastWall(int x, int y, int level, Transform storyFolder, Transform wall)
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

    private void PlaceNorthWall(int x, int y, int level, Transform storyFolder, Transform wall)
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

    private void PlaceWestWall(int x, int y, int level, Transform storyFolder, Transform wall)
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

    private void RenderRoofOnTop(Wing wing, Transform wingFolder)
    {
        for (int x = wing.Bounds.min.x; x < wing.Bounds.max.x; x++)
        {
            for (int y = wing.Bounds.min.y; y < wing.Bounds.max.y; y++)
            {
                PlaceRoof(x, y, wing.Stories.Length, wingFolder, wing.GetRoof.Type, wing.GetRoof.Direction);
            }
        }
    }
    
    private void RenderRoofOnTopWithDynamicSize(Wing wing, Transform wingFolder)
    {
        for (int x = wing.Stories[wing.Stories.Length-1].Bounds.min.x; x < wing.Stories[wing.Stories.Length-1].Bounds.max.x; x++)
        {
            for (int y = wing.Stories[wing.Stories.Length-1].Bounds.min.y; y < wing.Stories[wing.Stories.Length-1].Bounds.max.y; y++)
            {
                PlaceRoof(x, y, wing.Stories.Length, wingFolder, wing.GetRoof.Type, wing.GetRoof.Direction);
            }
        }
    }
    
    private void RenderRoofToEveryDynamicSizedStory(Wing wing, Transform wingFolder)
    {
        
        if (wing.Stories.Length < 2)
        {
            RenderRoofOnTop(wing, wingFolder);
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

        foreach (var story in storiesThatNeedRoof)
        {
            for (int x = story.Bounds.min.x; x < story.Bounds.max.x; x++)
            {
                for (int y = story.Bounds.min.y; y < story.Bounds.max.y; y++)
                {
                    PlaceRoof(x, y, story.Level + 1, wingFolder, wing.GetRoof.Type, wing.GetRoof.Direction);
                }
            }
        }
        RenderRoofOnTopWithDynamicSize(wing, wingFolder);
    }

    private void PlaceRoof(int x, int y, int level, Transform wingFolder, RoofType type, RoofDirection direction)
    {
        Transform r;
        r = new Transform(
            roofPrefab[(int)type],
            wingFolder.TransformPoint(
                new Vector3(
                        x * wallWidth,
                        level * wallHeight, //+ (type == RoofType.Point ? -0.3f : 0f),
                        y * wallWidth
                    )
                ),
            //Quaternion.Euler(0f, rotationOffset[(int)direction].y, 0f)
            Quaternion.Identity
            );
        r.SetParent(wingFolder);
        
        placedPrefabs.Add(r);
    }

    Vector3[] rotationOffset = {
        new Vector3 (-0f, 270f, 0f),
        new Vector3 (0f, 0f, 0f),
        new Vector3 (0f, 90, -0f),
        new Vector3 (-0f, 180, -0f)
    };

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
    public void SaveBuildingToObj()
    {
        var objStringToSave = StringifyBuilding();
        using (StreamWriter writer = new StreamWriter(File.Open("../../../Generated/building.obj", System.IO.FileMode.Append)))
        {
            writer.WriteAsync(objStringToSave);
        }
        Console.WriteLine("Saved building to obj file!");
    }
    
    public void SaveBuildingToObj(string objStringToSave)
    {
        using (StreamWriter writer = new StreamWriter(File.Open("../../../Generated/building.obj", System.IO.FileMode.Append)))
        {
            writer.WriteAsync(objStringToSave);
        }
    }

    public void SaveBuilding()
    {
        Console.WriteLine("Serializing building...");
        using (StreamWriter writer = new StreamWriter(File.Open("../../../Generated/building.obj", System.IO.FileMode.Append)))
        {
            
            foreach (var prefab in placedPrefabs)
            {
                writer.WriteAsync(prefab.VerticesToString());
                Console.WriteLine("Serialized " + prefab.Name + "\n");
            }

            foreach (var prefab in placedPrefabs)
            {
                writer.WriteAsync(prefab.FacesToString());
            }
            
            Console.WriteLine("Serialization complete!");
            //writer.WriteAsync(objStringToSave);
        }
    }
    
    public void UnRenderBuilding()
    {
        //var b = bldgFolder.gameObject;
        //DestroyImmediate(b);
        
    }
    
}