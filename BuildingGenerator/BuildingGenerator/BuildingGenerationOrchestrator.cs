using BuildingGenerator.Prefabs.Floors;
using BuildingGenerator.Prefabs.Roofs;
using BuildingGenerator.Prefabs.Walls;
using BuildingGenerator.Shared;
using System.Collections;
using System.Collections.Generic;

public class BuildingGenerationOrchestrator
{
    public BuildingSettings settings;
    private GenerationParams genParams = null;
    private Building b = null;
    
    public void GenerateBuilding()
    {
        genParams = new GenerationParams();
        genParams.BoundingBox = settings.Bounds;
        
        b = BuildingGeneration.Generate(settings, genParams);
        var serializer = new BuildingSerializer();

        var floorPrefab = new BasicFloor();
        var roofPrefab = new BasicPyramidRoof();
        var wallPrefab = new BasicWall();

        serializer.floorPrefab = floorPrefab.GetTransform();
        serializer.roofPrefab = new Transform[] { roofPrefab.GetTransform(), roofPrefab.GetTransform(), roofPrefab.GetTransform() };
        serializer.wallPrefab = new Transform[] { wallPrefab.GetTransform(), wallPrefab.GetTransform(), wallPrefab.GetTransform() };

        serializer.SerializeToObj(b);
        serializer.SaveBuilding();
        //new BuildingRenderer().Render(b);

    }
}
