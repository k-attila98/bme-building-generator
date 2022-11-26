// See https://aka.ms/new-console-template for more information

using BuildingGenerator.Shared;

Console.WriteLine("Building Generator v0.1");

var test = new BuildingGeneratorTest();
var settings = new BuildingSettings();
settings.Size = new Vector2Int(1, 1);
settings.Stories = 1;
settings.Wings = 1;
test.settings = settings;


test.GenerateBuilding();
