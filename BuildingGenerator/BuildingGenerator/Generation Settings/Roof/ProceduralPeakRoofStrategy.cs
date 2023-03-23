using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ProceduralPeakRoofStrategy : RoofStrategy
{
    public override Roof GenerateRoof(BuildingSettings settings, GenerationParams genParams)
    {
        Roof ret = new Roof(RoofType.ProceduralPeak, RoofDirection.North);
        return ret;
    }
}

