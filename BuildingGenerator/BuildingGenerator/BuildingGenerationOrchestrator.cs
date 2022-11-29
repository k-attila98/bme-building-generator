using BuildingGenerator.Prefabs.Floors;
using BuildingGenerator.Prefabs.Roofs;
using BuildingGenerator.Prefabs.Walls;
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
        b = BuildingGeneration.Generate(settings, genParams != null ? genParams : new GenerationParams());
        var serializer = new BuildingSerializer();

        var floorPrefab = new BasicFloor();
        var roofPrefab = new BasicPyramidRoof();
        var wallPrefab = new BasicWall();

        serializer.floorPrefab = floorPrefab.GetTransform();
        serializer.roofPrefab = new Transform[] { roofPrefab.GetTransform(), roofPrefab.GetTransform(), roofPrefab.GetTransform() };
        serializer.wallPrefab = new Transform[] { wallPrefab.GetTransform(), wallPrefab.GetTransform(), wallPrefab.GetTransform() };

        serializer.SerializeToObj(b);

    }
}
