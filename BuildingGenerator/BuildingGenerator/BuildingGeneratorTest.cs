using System.Collections;
using System.Collections.Generic;

public class BuildingGeneratorTest
{
    public BuildingSettings settings;
    private GenerationParams genParams = null;
    private Building b = null;
    
    public void Start()
    {
        genParams = new GenerationParams();
        genParams.BoundingBox = settings.Bounds;
        
        b = BuildingGenerator_v2.Generate(settings, genParams);
        var serializer = new BuildingSerializer();

        var obj = serializer.StringifyBuilding();
        serializer.SaveBuildingToObj(obj);
        //new BuildingRenderer().Render(b);

    }
    
    public void GenerateBuilding()
    {
        genParams = new GenerationParams();
        genParams.BoundingBox = settings.Bounds;
        
        b = BuildingGenerator_v2.Generate(settings, genParams);
        var serializer = new BuildingSerializer();
        serializer.SerializeToObj(b);
        serializer.SaveBuildingToObj();
        //new BuildingRenderer().Render(b);

    }
    
    public void DestroyBuilding()
    {
        //new BuildingRenderer().UnRenderBuilding();
    }
    
}
