using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.Shared
{
    public static class MathHelper
    {
        public static float GetRadFromDeg(float deg)
        {
            return (float)(deg * Math.PI / 180);
        }

        public static float GetDegFromRad(float rad)
        {
            return (float)(rad * 180 / Math.PI);
        }

        public static float RoundToNearestFloatWithDecimals(float numberToRound, int decimalCount)
        {
            return (float)Math.Round(numberToRound, decimalCount);
        }

        public static float RoundToNearestFloatWithDecimals(double numberToRound, int decimalCount)
        {
            return (float)Math.Round(numberToRound, decimalCount);
        }
    }
}
