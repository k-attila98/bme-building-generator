using BuildingGenerator.Shared;
using System.Collections;
using System.Collections.Generic;

public class GenerationParams 
{
    public int StoryNumber { get; set; }
    public RectInt BoundingBox { get; set; }
    
    public GenerationParams()
    {
        this.StoryNumber = 0;
        this.BoundingBox = new RectInt(new Vector2Int(0,0),new Vector2Int(0,0));
    }
}
