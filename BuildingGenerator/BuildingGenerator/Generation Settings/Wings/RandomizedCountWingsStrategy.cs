using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building generation/Wings/Randomized Count Wings Strategy")]
public class RandomizedCountWingsStrategy : WingsStrategy
{
    public override Wing[] GenerateWings(BuildingSettings settings, GenerationParams genParams)
    {
        float wingsRnd = UnityEngine.Random.Range(1, settings.buildingWingCount);
        int wingsInt = Mathf.RoundToInt(wingsRnd);

        Wing[] wings = new Wing[wingsInt];
        for (int i = 0; i < wingsInt; i++)
        {
            if (i > 0)
            {
                float wingXOffsetRnd = UnityEngine.Random.Range(-settings.Bounds.xMax + 1, settings.Bounds.xMax - 1);
                float wingYOffsetRnd = UnityEngine.Random.Range(-settings.Bounds.yMax + 1, settings.Bounds.yMax - 1);
        
                int wingsXOffsetInt = Mathf.RoundToInt(wingXOffsetRnd);
                int wingsYOffsetInt = Mathf.RoundToInt(wingYOffsetRnd);
                
                genParams.BoundingBox = new RectInt(new Vector2Int(wingsXOffsetInt, wingsYOffsetInt), settings.Bounds.size);
            }

            wings[i] = settings.wingStrategy != null
                ? settings.wingStrategy.GenerateWing(settings, genParams)
                : ScriptableObject.CreateInstance<DefaultWingStrategy>().GenerateWing(settings, genParams);
        }

        return wings;
    }
}