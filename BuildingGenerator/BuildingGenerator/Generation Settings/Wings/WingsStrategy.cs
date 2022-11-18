using System.Collections;
using System.Collections.Generic;

public abstract class WingsStrategy
{
    public abstract Wing[] GenerateWings(BuildingSettings settings, GenerationParams genParams);
}
