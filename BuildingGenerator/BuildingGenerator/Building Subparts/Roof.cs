using BuildingGenerator.Serialization.Interfaces;
using System.Collections;
using System.Collections.Generic;

public class Roof
{
    public RoofType Type { get; private set; }
    public RoofDirection Direction { get; private set; }

    public Roof(RoofType type = RoofType.Peak, RoofDirection direction = RoofDirection.North)
    {
        this.Type = type;
        this.Direction = direction;

    }

    public override string ToString()
    {
        return "Roof: " + Type.ToString() + "; " + Direction.ToString();
    }
}

public enum RoofType
{
    Point,
    Peak,
    Slope,
    Flat,
    ProceduralPeak
}

public enum RoofDirection
{
    North,  // positive y
    East,   // positive x
    South,  // negative y
    West    // negative x
}
