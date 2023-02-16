using BuildingGenerator.BuildingGenerator;
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

    public BuildingGenerationOrchestrator(BuildingSettings settings, GenerationParams? genParams = null)
    {
        this.settings = settings;
        this.genParams = new GenerationParams
        {
            BoundingBox = settings.Bounds
        };
    }
    
    public void GenerateBuilding()
    {
        VertexIdProvider.Reset();
        b = BuildingGeneration.Generate(settings, genParams != null ? genParams : new GenerationParams());
        var prefabPlacer = new BuildingPrefabPlacer();
        var serializer = new BuildingSerializer();

        var floorPrefab = new BasicFloor();
        var roofPrefab = new BasicPyramidRoof();
        var wallPrefab = new BasicWall();

        prefabPlacer.floorPrefab = floorPrefab.GetTransform();
        prefabPlacer.roofPrefab = new Transform[] { roofPrefab.GetTransform(), roofPrefab.GetTransform(), roofPrefab.GetTransform() };
        prefabPlacer.wallPrefab = new Transform[] { wallPrefab.GetTransform(), wallPrefab.GetTransform(), wallPrefab.GetTransform() };
        var placedPrefabs = prefabPlacer.PlacePrefabs(b);

        serializer.SaveBuilding(placedPrefabs);

    }

    public string GenerateBuildingToDisplay()
    {
        VertexIdProvider.Reset();
        b = BuildingGeneration.Generate(settings, genParams != null ? genParams : new GenerationParams());
        var prefabPlacer = new BuildingPrefabPlacer();
        var serializer = new BuildingSerializer();

        var floorPrefab = new BasicFloor();
        var roofPrefab = new BasicPyramidRoof();
        var wallPrefab = new BasicWall();

        prefabPlacer.floorPrefab = floorPrefab.GetTransform();
        prefabPlacer.roofPrefab = new Transform[] { roofPrefab.GetTransform(), roofPrefab.GetTransform(), roofPrefab.GetTransform() };
        prefabPlacer.wallPrefab = new Transform[] { wallPrefab.GetTransform(), wallPrefab.GetTransform(), wallPrefab.GetTransform() };
        var placedPrefabs = prefabPlacer.PlacePrefabs(b);

        return serializer.StringifyBuilding(placedPrefabs);

    }

    public void SerializeBuildingFromStr(string bldgStr)
    {
        var serializer = new BuildingSerializer();
        serializer.SaveBuilding(bldgStr);
    }
}
