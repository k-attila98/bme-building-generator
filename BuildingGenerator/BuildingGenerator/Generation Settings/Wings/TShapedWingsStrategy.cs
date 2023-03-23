using BuildingGenerator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TShapedWingsStrategy : WingsStrategy
{
    public override Wing[] GenerateWings(BuildingSettings settings, GenerationParams genParams)
    {
        if (settings.Size.x > settings.Size.y)
        {
            settings.Size = new Vector2Int(settings.Size.y, settings.Size.x);
            genParams.BoundingBox = settings.Bounds;
        }

        Wing[] wings = new Wing[2];
        for (int i = 0; i < 2; i++)
        {
            if (i > 0)
            {
                int wingsXOffsetInt = (-settings.Bounds.yMax / 2);
                int wingsYOffsetInt = settings.Bounds.yMax;

                genParams.BoundingBox = new RectInt(new Vector2Int(wingsXOffsetInt, wingsYOffsetInt), new Vector2Int(settings.Bounds.size.y, settings.Bounds.size.x));
            }

            wings[i] = settings.wingStrategy != null
                ? settings.wingStrategy.GenerateWing(settings, genParams)
                : new DefaultWingStrategy().GenerateWing(settings, genParams);
        }

        return wings;
    }
}

