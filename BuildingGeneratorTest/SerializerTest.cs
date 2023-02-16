using BuildingGenerator.BuildingGenerator;
using BuildingGenerator.Prefabs.Floors;
using BuildingGenerator.Prefabs.Roofs;
using BuildingGenerator.Prefabs.Walls;
using BuildingGenerator.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGeneratorTest
{
    [TestClass]
    public class SerializerTest
    {
        [TestMethod]
        public void TestSerialize()
        {
            /*
            var serializer = new BuildingGenerator.Serializer();
            var building = new BuildingGenerator.Building();
            var serialized = serializer.Serialize(building);
            Assert.AreEqual(serialized, "Building");
            */
            var settings = new BuildingSettings();
            settings.Size = new Vector2Int(1, 1);
            settings.Stories = new Vector2Int(1,1);
            settings.Wings = new Vector2Int(1, 1);
            var genParams = new GenerationParams();
            genParams.BoundingBox = settings.Bounds;

            Building b = BuildingGeneration.Generate(settings, genParams);
            var prefabPlacer = new BuildingPrefabPlacer();
            var serializer = new BuildingSerializer();

            var floorPrefab = new BasicFloor();
            var roofPrefab = new BasicPyramidRoof();
            var wallPrefab = new BasicWall();

            prefabPlacer.floorPrefab = floorPrefab.GetTransform();
            prefabPlacer.roofPrefab = new Transform[] { roofPrefab.GetTransform(), roofPrefab.GetTransform(), roofPrefab.GetTransform() };
            prefabPlacer.wallPrefab = new Transform[] { wallPrefab.GetTransform(), wallPrefab.GetTransform(), wallPrefab.GetTransform() };
            var placedPrefabs = prefabPlacer.PlacePrefabs(b);
 
            var result = serializer.StringifyBuilding(placedPrefabs);

            //TODO: ezt megírni xd
            /*
            Assert.AreEqual(result, "" +
                "v 0 0 0" +
                "");
            */
            Console.WriteLine(result);

            Assert.AreEqual(true, false);
        }
    }
}
