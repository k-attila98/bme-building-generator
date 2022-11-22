using BuildingGenerator.Shared;
using System.Collections;
using System.Collections.Generic;

public class RandomizedCountWingsStrategy : WingsStrategy
{
    public override Wing[] GenerateWings(BuildingSettings settings, GenerationParams genParams)
    {
        Random random = new Random();
        int wingsInt = random.Next(settings.buildingWingCount + 1);

        Wing[] wings = new Wing[wingsInt];
        for (int i = 0; i < wingsInt; i++)
        {
            if (i > 0)
            {
                int wingsXOffsetInt = random.Next(-settings.Bounds.xMax + 1, settings.Bounds.xMax - 1);
                int wingsYOffsetInt = random.Next(-settings.Bounds.yMax + 1, settings.Bounds.yMax - 1);
                
                genParams.BoundingBox = new RectInt(new Vector2Int(wingsXOffsetInt, wingsYOffsetInt), settings.Bounds.size);
            }

            wings[i] = settings.wingStrategy != null
                ? settings.wingStrategy.GenerateWing(settings, genParams)
                : new DefaultWingStrategy().GenerateWing(settings, genParams);
        }

        return wings;
    }
}