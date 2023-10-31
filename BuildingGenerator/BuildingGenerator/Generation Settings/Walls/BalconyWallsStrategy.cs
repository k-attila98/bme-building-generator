using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.BuildingGenerator.Generation_Settings.Walls
{
    internal class BalconyWallsStrategy : WallsStrategy
    {
        public override Wall[] GenerateWalls(BuildingSettings settings, GenerationParams genParams)
        {
            int perimeterSize = ((genParams.BoundingBox.size.x + genParams.BoundingBox.size.y) * 2);
            int innerPartSize = (((genParams.BoundingBox.size.x - 2) + (genParams.BoundingBox.size.y - 2)) * 2);
            Wall[] walls = new Wall[perimeterSize + innerPartSize];

            for (int i = 0; i < perimeterSize; i++)
            {
                walls[i] = Wall.Half;
            }

            for (int i = 0; i < innerPartSize; i++)
            {
                walls[i] = Wall.Standard;
            }

            return walls;
        }
    }
}
