using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex = BuildingGenerator.Shared.Vertex;
using Vector3 = BuildingGenerator.Shared.Vector3;

namespace BuildingGeneratorTest
{
    [TestClass]
    public class VertexTest
    {
        [TestMethod]
        public void TestVertex()
        {
            var vertex = new Vertex(new Vector3(1, 2, 3));
            Assert.AreEqual(1, vertex.Position.x);
            Assert.AreEqual(2, vertex.Position.y);
            Assert.AreEqual(3, vertex.Position.z);
        }

        [TestMethod]
        public void TestVertexId()
        {
            var vertex1 = new Vertex(new Vector3(1, 2, 3));
            var vertex2 = new Vertex(new Vector3(1, 2, 3));
            Assert.AreEqual(vertex1.Id, vertex2.Id);
        }

        [TestMethod]
        public void TestVertexClone()
        {
            var vertex = new Vertex(new Vector3(1, 2, 3));
            var vertexClone = vertex.Clone(true);
            Assert.AreEqual(vertex.Id, vertexClone.Id);
            Assert.AreEqual(vertex.Position.x, vertexClone.Position.x);
            Assert.AreEqual(vertex.Position.y, vertexClone.Position.y);
            Assert.AreEqual(vertex.Position.z, vertexClone.Position.z);
        }

        [TestMethod]
        public void TestVertexAdd()
        {
            var vertex = new Vertex(new Vector3(1, 2, 3));
            vertex.Add(new Vector3(4, 5, 6));
            Assert.AreEqual(5, vertex.Position.x);
            Assert.AreEqual(7, vertex.Position.y);
            Assert.AreEqual(9, vertex.Position.z);
        }

        [TestMethod]
        public void TestVertexSubtract()
        {
            var vertex = new Vertex(new Vector3(1, 2, 3));
            vertex.Subtract(new Vector3(4, 5, 6));
            Assert.AreEqual(-3, vertex.Position.x);
            Assert.AreEqual(-3, vertex.Position.y);
            Assert.AreEqual(-3, vertex.Position.z);
        }

        [TestMethod]
        public void TestVertexAddVertex()
        {
            var vertex1 = new Vertex(new Vector3(1, 2, 3));
            var vertex2 = new Vertex(new Vector3(4, 5, 6));
            vertex1.Add(vertex2);
            Assert.AreEqual(5, vertex1.Position.x);
            Assert.AreEqual(7, vertex1.Position.y);
            Assert.AreEqual(9, vertex1.Position.z);
        }

        [TestMethod]
        public void TestVertexSubtractVertex()
        {
            var vertex1 = new Vertex(new Vector3(1, 2, 3));
            var vertex2 = new Vertex(new Vector3(4, 5, 6));
            vertex1.Subtract(vertex2);
            Assert.AreEqual(-3, vertex1.Position.x);
            Assert.AreEqual(-3, vertex1.Position.y);
            Assert.AreEqual(-3, vertex1.Position.z);
        }

        [TestMethod]
        public void TestVertexCloneAddVector()
        {
            var vertex = new Vertex(new Vector3(1, 2, 3));
            var vertexClone = vertex.Clone(true);
            vertexClone.Add(new Vector3(4, 5, 6));
            Assert.AreEqual(1, vertex.Position.x);
            Assert.AreEqual(2, vertex.Position.y);
            Assert.AreEqual(3, vertex.Position.z);
            Assert.AreEqual(5, vertexClone.Position.x);
            Assert.AreEqual(7, vertexClone.Position.y);
            Assert.AreEqual(9, vertexClone.Position.z);
            Assert.AreEqual(vertex.Id, vertexClone.Id);
        }
    
    }
}
