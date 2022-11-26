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
    public int buildingStoryCount;
    public int buildingWingCount;

    public Vector2Int Size
    {
        get { return buildingSize; }
        set { buildingSize = value; }
    }
    
    public int Stories
    {
        get { return buildingStoryCount; }
        set { buildingStoryCount = value; }
    }
    
    public int Wings
    {
        get { return buildingWingCount; }
        set { buildingWingCount = value; }
    }

    public RectInt Bounds
    {
        get { return new RectInt(0, 0, buildingSize.x, buildingSize.y); }
    }
    
    
}
