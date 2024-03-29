using BuildingGenerator.Serialization;
using BuildingGenerator.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Vector3 = BuildingGenerator.Shared.Vector3;

public class BuildingSerializer
{
    public void SaveBuilding(List<Transform> placedPrefabs)
    {
        Console.WriteLine("Serializing building...");
        using (StreamWriter writer = new StreamWriter(File.Open($"../../../Generated/building-{DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss")}.obj", System.IO.FileMode.Create)))
        {

            foreach (var prefab in placedPrefabs)
            {
                writer.Write(prefab.VerticesToString());
                Console.WriteLine("Serialized " + prefab.Name + "\n");
            }

            foreach (var prefab in placedPrefabs)
            {
                writer.Write(prefab.FacesToString());
            }

            Console.WriteLine("Serialization complete!");
        }
    }

    public void SaveBuildingObj(List<Transform> placedPrefabs)
    {
        Console.WriteLine("Serializing building...");

        Obj obj = new Obj(placedPrefabs);
        obj.WriteObjFile(null);

        Console.WriteLine("Serialization complete!");
    }

    public void SaveBuilding(string objStr)
    {
        Console.WriteLine("Serializing building...");
        using (StreamWriter writer = new StreamWriter(File.Open($"../../../../BuildingGenerator/Generated/building-{DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss")}.obj", System.IO.FileMode.Create)))
        {
            writer.Write(objStr);
            Console.WriteLine("Serialization complete!");
        }
    }

    /**
     * Needed for the wpf app, need to return the string instead of writing it to a file for displaying it on the screen
     */
    public string StringifyBuilding(List<Transform> placedPrefabs)
    {
        string objFileContent = "";
        Console.WriteLine("Serializing building...");

        foreach (var prefab in placedPrefabs)
        {
            objFileContent += prefab.VerticesToString();
            Console.WriteLine("Serialized " + prefab.Name + "\n");
        }

        foreach (var prefab in placedPrefabs)
        {
            objFileContent += prefab.FacesToString();
        }
        Console.WriteLine("Serialization complete!");
        return objFileContent;
    }
    
    public Dictionary<string, string> StringifyObjSorted(List<Transform> placedPrefabs)
    {
        string objFileContent = "";
        Console.WriteLine("Serializing building...");


        Dictionary<string, string> stringifiedSortedPrefabs = new Dictionary<string, string>();
        Dictionary<string, List<Transform>> sorted = _SortTransformsByTexture(placedPrefabs);

        foreach (var key in sorted.Keys)
        {
            VertexIdProvider.Reset();
            stringifiedSortedPrefabs.Add(key, StringifyObj(sorted[key]));
        }

        Console.WriteLine("Serialization complete!");
        return stringifiedSortedPrefabs;
    }

    public string StringifyObj(List<Transform> placedPrefabs)
    {
        string objFileContent = "";
        Console.WriteLine("Serializing building...");

        Obj obj = new Obj(placedPrefabs);
        objFileContent = obj.WriteObjString(null);

        Console.WriteLine("Serialization complete!");
        return objFileContent;
    }

    public string StringifyObj(List<Transform> placedRoofs, List<Transform> placedWalls, List<Transform> placedFloors)
    {
        string objFileContent = "";
        Console.WriteLine("Serializing building...");

        Obj obj = new Obj(placedFloors);
        objFileContent = obj.WriteObjString(null);
        obj = new Obj(placedWalls);
        objFileContent += obj.WriteObjString(null);
        obj = new Obj(placedRoofs);
        objFileContent += obj.WriteObjString(null);

        Console.WriteLine("Serialization complete!");
        return objFileContent;
    }

    private Dictionary<string, List<Transform>> _SortTransformsByTexture(List<Transform> transformsToSort)
    {
        Dictionary<string, List<Transform>> sortedTransforms = new Dictionary<string, List<Transform>>();
        foreach (var transform in transformsToSort)
        {
            if (transform.Mtl == null)
            {
                if (sortedTransforms.ContainsKey("null"))
                {
                    sortedTransforms["null"].Add(transform);
                }
                else
                {
                    sortedTransforms.Add("null", new List<Transform> { transform });
                }
                continue;
            }
            var materialPath = Path.Combine(Path.GetDirectoryName(transform.LoadPath) ?? ".", transform.UseMtl + ".png");

            if (sortedTransforms.ContainsKey(materialPath))
            {
                sortedTransforms[materialPath].Add(transform);
            }
            else
            {
                sortedTransforms.Add(materialPath, new List<Transform> { transform });
            }
        }

        return sortedTransforms;
    }
    

    public Transform ReadTransform(string path)
    { 
        Obj obj = new Obj();
        obj.LoadObj(path);
        return obj.ToTransform();

    }
}