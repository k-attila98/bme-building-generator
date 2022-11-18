using BuildingGenerator.Serialization.Interfaces;
using BuildingGenerator.Shared;
using System.Collections;
using System.Collections.Generic;

public class Wing
{
    public RectInt Bounds { get; private set; }
    public Story[] Stories { get; private set; }
    public Roof GetRoof { get; private set; }

    public Wing(RectInt bounds)
    {
        this.Bounds = bounds;
    }

    public Wing(RectInt bounds, Story[] stories, Roof roof)
    {
        this.Bounds = bounds;
        this.Stories = stories;
        this.GetRoof = roof;
    }

    public override string ToString()
    {
        string wing = "Wing(" + Bounds.ToString() + "):\n";
        foreach (var s in Stories)
        {
            wing += "\t" + s.ToString() + "\n";
        }

        wing += "\t" + GetRoof.ToString() + "\n";
        return wing;
    }
}
