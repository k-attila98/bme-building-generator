using System.Collections;
using System.Collections.Generic;

public abstract class WingStrategy
{
    public abstract Wing GenerateWing(BuildingSettings settings, GenerationParams genParams);
} 
