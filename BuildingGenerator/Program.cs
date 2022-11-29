// See https://aka.ms/new-console-template for more information

using BuildingGenerator.Prefabs.Floors;
using BuildingGenerator.Prefabs.Roofs;
using BuildingGenerator.Prefabs.Walls;
using BuildingGenerator.Shared;

Console.WriteLine("Building Generator v0.1");

var generator = new BuildingGenerationOrchestrator();
var settings = new BuildingSettings();
settings.Size = new Vector2Int(2, 3);
settings.Stories = 1;
settings.Wings = 1;
var genParams = new GenerationParams();
genParams.BoundingBox = settings.Bounds;

generator.settings = settings;
generator.GenerateBuilding();
