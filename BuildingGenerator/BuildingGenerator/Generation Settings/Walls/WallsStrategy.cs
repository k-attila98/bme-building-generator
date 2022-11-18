using System.Collections;
using System.Collections.Generic;

public abstract class WallsStrategy
{
    public abstract Wall[] GenerateWalls(BuildingSettings settings, GenerationParams genParams);
}
