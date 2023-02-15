using BuildingGenerator.Shared;
using System.Collections;
using System.Collections.Generic;

public class BuildingSettings
{
    public RoofStrategy roofStrategy;
    public WallsStrategy wallsStrategy;
    public StoryStrategy storyStrategy;
    public StoriesStrategy storiesStrategy;
    public WingStrategy wingStrategy;
    public WingsStrategy wingsStrategy;
    
    public Vector2Int buildingSize;
    public Vector2Int buildingStoryCount;
    public Vector2Int buildingWingCount;

    public Vector2Int Size
    {
        get { return buildingSize; }
        set { buildingSize = value; }
    }
    
    public int StoriesMin
    {
        get { return buildingStoryCount.x; }
        set { buildingStoryCount.x = value; }
    }

    public int StoriesMax
    {
        get { return buildingStoryCount.y; }
        set { buildingStoryCount.y = value; }
    }

    public Vector2Int Stories
    {
        get { return buildingStoryCount; }
        set { buildingStoryCount = value; }
    }

    public Vector2Int Wings
    {
        get { return buildingWingCount; }
        set { buildingWingCount = value; }
    }

    public int WingsMin
    {
        get { return buildingWingCount.x; }
        set { buildingWingCount.x = value; }
    }

    public int WingsMax
    {
        get { return buildingWingCount.y; }
        set { buildingWingCount.y = value; }
    }

    public RectInt Bounds
    {
        get { return new RectInt(0, 0, buildingSize.x, buildingSize.y); }
    }
    
    
}
