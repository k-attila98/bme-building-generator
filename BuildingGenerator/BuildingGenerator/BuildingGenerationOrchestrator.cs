using BuildingGenerator.BuildingGenerator;
using BuildingGenerator.Prefabs;
using BuildingGenerator.Prefabs.Floors;
using BuildingGenerator.Prefabs.Roofs;
using BuildingGenerator.Prefabs.Walls;
using BuildingGenerator.Serialization;
using BuildingGenerator.Shared;
using System.Collections;
using System.Collections.Generic;

public class BuildingGenerationOrchestrator
{
    private BuildingSettings settings;
    private GenerationParams? genParams = null;
    private Building? b = null;

    private Transform[] wallPrefabs = new Transform[0];
    private Transform[] roofPrefabs = new Transform[0];
    private Transform floorPrefab = new Transform();


    public BuildingGenerationOrchestrator()
    {

    }
    public BuildingGenerationOrchestrator(BuildingSettings settings, GenerationParams? genParams = null)
    {
        this.settings = settings;
        this.genParams = new GenerationParams
        {
            BoundingBox = settings.Bounds
        };
    }

    public void AddSettings(BuildingSettings settings, GenerationParams? genParams = null)
    {
        this.settings = settings;
        this.genParams = new GenerationParams
        {
            BoundingBox = settings.Bounds
        };
    }

    private void _SetPrefabPlacer(ref BuildingPrefabPlacer prefabPlacer, Transform floorPrefab, Transform[] roofPrefabs, Transform[] wallPrefabs)
    {
        prefabPlacer.floorPrefab = floorPrefab;
        prefabPlacer.roofPrefab = roofPrefabs.ToArray();
        prefabPlacer.wallPrefab = wallPrefabs.ToArray();
    }
    
    public void GenerateBuilding(bool isCustomPrefabs)
    {
        VertexIdProvider.Reset();
        b = BuildingGeneration.Generate(settings, genParams != null ? genParams : new GenerationParams());
        var prefabPlacer = new BuildingPrefabPlacer();
        var serializer = new BuildingSerializer();

        var floorPrefab = new BasicFloor();
        var roofPrefab = new BasicPyramidRoof();
        var wallPrefab = new BasicWall();

        if (isCustomPrefabs)
        {
            _SetPrefabPlacer(ref prefabPlacer, this.floorPrefab, roofPrefabs, wallPrefabs);
        }
        else 
        {
            _SetPrefabPlacer(ref prefabPlacer, floorPrefab.GetTransform(), new Transform[] { roofPrefab.GetTransform() }, new Transform[] { wallPrefab.GetTransform() });
        }
        var placedPrefabs = prefabPlacer.PlacePrefabs(b);

        serializer.SaveBuilding(placedPrefabs);

    }

    public string GenerateBuildingToDisplay(bool isCustomPrefabs)
    {
        VertexIdProvider.Reset();
        b = BuildingGeneration.Generate(settings, genParams != null ? genParams : new GenerationParams());
        var prefabPlacer = new BuildingPrefabPlacer();
        var serializer = new BuildingSerializer();

        var floorPrefab = new BasicFloor();
        var roofPrefab = new BasicPyramidRoof();
        var wallPrefab = new BasicWall();

        if (isCustomPrefabs)
        {
            _SetPrefabPlacer(ref prefabPlacer, this.floorPrefab, roofPrefabs, wallPrefabs);
        }
        else
        {
            _SetPrefabPlacer(ref prefabPlacer, floorPrefab.GetTransform(), new Transform[] { roofPrefab.GetTransform() }, new Transform[] { wallPrefab.GetTransform() });
        }
        var placedPrefabs = prefabPlacer.PlacePrefabs(b);

        return serializer.StringifyObj(placedPrefabs);

    }

    public void SerializeBuildingFromStr(string bldgStr)
    {
        var serializer = new BuildingSerializer();
        serializer.SaveBuilding(bldgStr);
    }

    public string DeserializePrefabFromObj(string path)
    {
        var data = File.ReadAllLines(path);
        string ret = string.Empty;
        
        foreach (var line in data)
        {
            ret += line;
            ret += "\n";
        }

        return ret;
    }

    public void DeserializeWallTransformFromObj(string path)
    { 
        var serializer = new BuildingSerializer();
        var prefab = serializer.ReadTransform(path);
        var walls = wallPrefabs.ToList();
        walls.Add(prefab);
        wallPrefabs = walls.ToArray();
    }

    public void DeserializeRoofTransformFromObj(string path)
    {
        var serializer = new BuildingSerializer();
        var prefab = serializer.ReadTransform(path);
        var roofs = wallPrefabs.ToList();
        roofs.Add(prefab);
        roofPrefabs = roofs.ToArray();
    }

    public void DeserializeFloorTransformFromObj(string path)
    {
        var serializer = new BuildingSerializer();
        var prefab = serializer.ReadTransform(path);
        floorPrefab = prefab;
    }
}
