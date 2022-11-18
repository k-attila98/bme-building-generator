using System.Collections;
using System.Collections.Generic;
public class DefaultRoofStrategy : RoofStrategy
{
    public override Roof GenerateRoof(BuildingSettings settings, GenerationParams genParams)
    {
        return new Roof();
    }
}
