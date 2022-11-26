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
            var genParams = new GenerationParams();
            genParams.BoundingBox = settings.Bounds;

            Building b = BuildingGeneration.Generate(settings, genParams);
            var serializer = new BuildingSerializer();

            var floorPrefab = new BasicFloor();
            var roofPrefab = new BasicPyramidRoof();
            var wallPrefab = new BasicWall();

            serializer.floorPrefab = floorPrefab.GetTransform();
            serializer.roofPrefab = new Transform[] { roofPrefab.GetTransform(), roofPrefab.GetTransform(), roofPrefab.GetTransform() };
            serializer.wallPrefab = new Transform[] { wallPrefab.GetTransform(), wallPrefab.GetTransform(), wallPrefab.GetTransform() };

            serializer.SerializeToObj(b);
            serializer.SaveBuildingToObj();
        }
    }
}
