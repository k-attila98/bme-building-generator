using System.Collections;
using System.Collections.Generic;

public static class BuildingGeneration
{
    public static Building Generate(BuildingSettings settings, GenerationParams genParams)
    {
        return new Building(
            settings.Size.x, 
            settings.Size.y,
            settings.wingsStrategy != null ?
                settings.wingsStrategy.GenerateWings(settings, genParams) : 
                new DefaultWingsStrategy().GenerateWings(settings, genParams)
            );
    }
}
