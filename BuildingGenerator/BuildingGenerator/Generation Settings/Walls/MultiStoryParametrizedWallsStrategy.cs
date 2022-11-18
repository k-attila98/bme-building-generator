using System.Collections;
using System.Collections.Generic;

public class MultiStoryParametrizedWallsStrategy : WallsStrategy
{
    public float StandardWallProb = 0f;
    public float WindowWallProb = 0f;
    public float DoorWallProb = 0f;
    public override Wall[] GenerateWalls(BuildingSettings settings, GenerationParams genParams)
    {
        Random random = new Random();
        int size = ((genParams.BoundingBox.size.x + genParams.BoundingBox.size.y) * 2);
        Wall[] walls = new Wall[size];
        int doorCount = 0;

        for (int i = 0; i < size; i++)
        {
            float prob = (float)random.NextDouble();
            
            if (prob < StandardWallProb)
                walls[i] = Wall.Standard;
            if (prob >= StandardWallProb && prob < StandardWallProb+WindowWallProb)
                walls[i] = Wall.Window;
            
            if (prob >= StandardWallProb+WindowWallProb && prob < StandardWallProb+WindowWallProb+DoorWallProb && doorCount < 1 && genParams.StoryNumber == 0)
            {
                walls[i] = Wall.Door;
                doorCount++;
                //Debug.Log("door created");
            }
            else if (prob >= WindowWallProb && doorCount >= 1)
            {
                //Debug.Log(genParams.StoryNumber);
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
