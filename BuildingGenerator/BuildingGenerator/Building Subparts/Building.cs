using BuildingGenerator.Serialization.Interfaces;
using BuildingGenerator.Shared;
using System.Collections;
using System.Collections.Generic;

public class Building
{
    
    public Vector2Int Size { get; private set; }
    public Wing[] Wings { get; private set; }

    public Building(int sizeX, int sizeY, Wing[] wings)
    {
        Size = new Vector2Int(sizeX, sizeY);
        this.Wings = wings;
    }

    public override string ToString()
    {
        string building = "Building:(" + Size.ToString() + "; " + Wings.Length +"):\n";
        foreach (var w in Wings)
        {
            building += "\t" + w.ToString() + "\n";
        }

        return building;
    }
}
