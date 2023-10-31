using BuildingGenerator.Serialization.Interfaces;
using BuildingGenerator.Shared;
using System.Collections;
using System.Collections.Generic;

public class Story
{
    public int Level { get; private set; }
    public Wall[] Walls { get; private set; }
    public RectInt Bounds { get; private set; }
    public bool IsHangingFloors { get; private set; }

    public Story(int level, Wall[] walls, bool isHangningFloors)
    {
        this.Level = level;
        this.Walls = walls;
        this.IsHangingFloors = isHangningFloors;
    }
    
    public Story(int level, Wall[] walls, RectInt bounds, bool isHangningFloors)
    {
        this.Level = level;
        this.Walls = walls;
        this.Bounds = bounds;
        this.IsHangingFloors = isHangningFloors;
    }

    public override string ToString()
    {
        string story = "Story(" + Level + "):\n";
        story += "\t\tWalls:";
        foreach (var w in Walls)
        {
            story += w.ToString() + ",";
        }
        return story;
    }
}
