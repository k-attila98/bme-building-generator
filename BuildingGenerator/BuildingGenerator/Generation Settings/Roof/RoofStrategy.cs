using System.Collections;
using System.Collections.Generic;

public abstract class RoofStrategy
{
    public abstract Roof GenerateRoof(BuildingSettings settings, GenerationParams genParams);
    
}
