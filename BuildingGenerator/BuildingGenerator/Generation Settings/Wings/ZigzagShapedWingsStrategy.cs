using BuildingGenerator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class ZigzagShapedWingsStrategy : WingsStrategy
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
                int wingsXOffsetInt = settings.Bounds.xMax;
                int wingsYOffsetInt = (settings.Bounds.yMax / 2);

                genParams.BoundingBox = new RectInt(new Vector2Int(wingsXOffsetInt, wingsYOffsetInt), settings.Size);
            }

            wings[i] = settings.wingStrategy != null
                ? settings.wingStrategy.GenerateWing(settings, genParams)
                : new DefaultWingStrategy().GenerateWing(settings, genParams);
        }

        return wings;
    }
}

