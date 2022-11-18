using System.Collections;
using System.Collections.Generic;

public class BuildingGeneratorTest
{
    public BuildingSettings settings;
    private GenerationParams genParams = null;
    private Building b = null;
    
    void Start()
    {
        genParams = new GenerationParams();
        genParams.BoundingBox = settings.Bounds;
        
        b = BuildingGenerator_v2.Generate(settings, genParams);
        Console.Write(b.ToString());
        //new BuildingRenderer().Render(b);
        
    }
    
    public void GenerateBuilding()
    {
        genParams = new GenerationParams();
        genParams.BoundingBox = settings.Bounds;
        
        b = BuildingGenerator_v2.Generate(settings, genParams);
        //new BuildingRenderer().Render(b);
        
    }
    
    public void DestroyBuilding()
    {
        //new BuildingRenderer().UnRenderBuilding();
    }
    
}
