// See https://aka.ms/new-console-template for more information

using BuildingGenerator.Prefabs.Floors;
using BuildingGenerator.Prefabs.Roofs;
using BuildingGenerator.Prefabs.Walls;
using BuildingGenerator.Shared;

Console.WriteLine("Building Generator v0.7");


var settings = new BuildingSettings();
settings.Size = new Vector2Int(4, 3);
settings.Stories = 5;
settings.Wings = 3;
settings.storiesStrategy = new RandomizedCountStoriesWithTerracesStrategy();
settings.wingsStrategy = new RandomizedCountWingsStrategy();

var generator = new BuildingGenerationOrchestrator(settings);

generator.GenerateBuilding();
