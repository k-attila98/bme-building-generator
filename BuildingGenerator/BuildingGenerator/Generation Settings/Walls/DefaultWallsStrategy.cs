using System.Collections;
using System.Collections.Generic;

public class DefaultWallsStrategy : WallsStrategy
{
    public override Wall[] GenerateWalls(BuildingSettings settings, GenerationParams genParams)
    {
        int size = ((genParams.BoundingBox.size.x + genParams.BoundingBox.size.y) * 2);
        Wall[] walls = new Wall[size];
        return walls;
    }
}
