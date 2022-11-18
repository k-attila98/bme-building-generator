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
    
    public Vector2Int Size
    {
        get { return buildingSize; }
    }
    
    public int buildingStoryCount;
    
    public int Stories
    {
        get { return buildingStoryCount; }
    }
    
    public int buildingWingCount;
    
    public int Wings
    {
        get { return buildingWingCount; }
    }

    public RectInt Bounds
    {
        get { return new RectInt(0, 0, buildingSize.x, buildingSize.y); }
    }
    
    
}
