using System;
using System.Collections;
using System.Collections.Generic;

public class ParametrizedWallsStrategy : WallsStrategy
{
    public override Wall[] GenerateWalls(BuildingSettings settings, GenerationParams genParams)
    {
        int size = ((genParams.BoundingBox.size.x + genParams.BoundingBox.size.y) * 2);
        Wall[] walls = new Wall[size];
        int doorCount = 0;

        for (int i = 0; i < size; i++)
        {
            Random random = new Random();
            float prob = (float)random.NextDouble();

            if (prob < 0.6f)
                walls[i] = Wall.Standard;
            if (prob >= 0.6f && prob < 0.9f)
                walls[i] = Wall.Window;
            
            if (prob >= 0.9f && doorCount < 1)
            {
                walls[i] = Wall.Door;
                doorCount++;
            }
            else if (prob >= 0.9f && doorCount >= 1)
            {
                float prob2 = (float)random.NextDouble();
                if (prob2 < 0.5f)
                    walls[i] = Wall.Standard;
                if (prob2 >= 0.5f)
                    walls[i] = Wall.Window;
            }
        }
        
        return walls;
    }
}
