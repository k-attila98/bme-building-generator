using BuildingGenerator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UShapedWingsStrategy : WingsStrategy
{
    public override Wing[] GenerateWings(BuildingSettings settings, GenerationParams genParams)
    {
        if (settings.Size.x > settings.Size.y)
        {
            settings.Size = new Vector2Int(settings.Size.y, settings.Size.x);
            genParams.BoundingBox = settings.Bounds;
        }

        Wing[] wings = new Wing[3];
        for (int i = 0; i < 3; i++)
        {

            switch (i)
            {
                case 0:
                    genParams.BoundingBox = settings.Bounds;
                    break;
                case 1:
                    int wingsXOffset1 = settings.Bounds.xMax - settings.Bounds.xMax / 2 - settings.Bounds.xMax % 2;
                    int wingsYOffset1 = settings.Bounds.yMax;
                    genParams.BoundingBox = new RectInt(new Vector2Int(wingsXOffset1, wingsYOffset1), new Vector2Int(settings.Bounds.size.y, settings.Bounds.size.x));
                    break;
                case 2:
                    int wingsXOffset2 = settings.Bounds.yMax - settings.Bounds.xMax % 2;
                    int wingsYOffset2 = 0;
                    genParams.BoundingBox = new RectInt(new Vector2Int(wingsXOffset2, wingsYOffset2), new Vector2Int(settings.Bounds.size.x, settings.Bounds.size.y));
                    break;
                default:
                    break;
            }

            wings[i] = settings.wingStrategy != null
                ? settings.wingStrategy.GenerateWing(settings, genParams)
                : new DefaultWingStrategy().GenerateWing(settings, genParams);
        }

        return wings;
    }
}

