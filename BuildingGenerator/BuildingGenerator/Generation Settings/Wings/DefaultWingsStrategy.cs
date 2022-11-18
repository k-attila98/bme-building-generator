using System.Collections;
using System.Collections.Generic;

public class DefaultWingsStrategy : WingsStrategy
{
    public override Wing[] GenerateWings(BuildingSettings settings, GenerationParams genParams)
    {
        return new Wing[]
        {
            //GenerateWing(settings)
            settings.wingStrategy != null ?
                settings.wingStrategy.GenerateWing(settings, genParams) : 
                new DefaultWingStrategy().GenerateWing(settings, genParams)
        };
    }
}
