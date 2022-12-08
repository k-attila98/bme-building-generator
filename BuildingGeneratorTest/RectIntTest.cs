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
    public class RectIntTest
    {
        [TestMethod]
        public void TestOverlapBothPositive()
        {
            var rect1 = new RectInt(0, 0, 10, 10);
            var rect2 = new RectInt(5, 5, 10, 10);

            Assert.IsTrue(rect1.Overlaps(rect2));
        }

        [TestMethod]
        public void TestOverlapBothNegative()
        {
            var rect1 = new RectInt(-10, -10, -10, -10);
            var rect2 = new RectInt(-5, -5, -10, -10);

            Assert.IsTrue(rect1.Overlaps(rect2));
        }

        [TestMethod]
        public void TestOverlapOnePositiveOneNegative()
        {
            var rect1 = new RectInt(-10, -10, -10, -10);
            var rect2 = new RectInt(5, 5, 10, 10);
            var rect3 = new RectInt(-5, -5, 15, 15);

            Assert.IsFalse(rect1.Overlaps(rect2));
            Assert.IsTrue(rect2.Overlaps(rect3));
        }

        [TestMethod]
        public void TestOverlapOneInsideOther()
        {
            var rect1 = new RectInt(0, 0, 10, 10);
            var rect2 = new RectInt(5, 5, 5, 5);

            Assert.IsTrue(rect1.Overlaps(rect2));
        }

        [TestMethod]
        public void TestOverlapOneInsideOtherNegative()
        {
            var rect1 = new RectInt(-10, -10, -10, -10);
            var rect2 = new RectInt(-5, -5, -5, -5);

            Assert.IsFalse(rect1.Overlaps(rect2));
        }

        [TestMethod]
        public void TestOverlapOneInsideOtherOnePositiveOneNegative()
        {
            var rect1 = new RectInt(-10, -10, -10, -10);
            var rect2 = new RectInt(5, 5, 5, 5);
            var rect3 = new RectInt(-5, -5, 15, 15);

            Assert.IsFalse(rect1.Overlaps(rect2));
            Assert.IsTrue(rect2.Overlaps(rect3));
        }

        [TestMethod]
        public void TestIntersectionBothPositive()
        {
            var rect1 = new RectInt(0, 0, 10, 10);
            var rect2 = new RectInt(5, 5, 10, 10);

            var intersection = rect1.Intersect(rect2);

            Assert.AreEqual(5, intersection.x);
            Assert.AreEqual(5, intersection.y);
            Assert.AreEqual(5, intersection.width);
            Assert.AreEqual(5, intersection.height);
        }

        [TestMethod]
        public void TestIntersectionBothNegative()
        {
            var rect1 = new RectInt(-10, -10, -10, -10);
            var rect2 = new RectInt(-5, -5, -10, -10);

            var intersection = rect1.Intersect(rect2);

            Assert.AreEqual(-15, intersection.x);
            Assert.AreEqual(-15, intersection.y);
            Assert.AreEqual(5, intersection.width);
            Assert.AreEqual(5, intersection.height);
        }

        [TestMethod]
        public void TestIntersectionOnePositiveOneNegative()
        {
            var rect1 = new RectInt(-10, -10, -10, -10);
            var rect2 = new RectInt(5, 5, 10, 10);
            var rect3 = new RectInt(-5, -5, 15, 15);

            
            var intersection = rect1.Intersect(rect2);
            Assert.AreEqual(0, intersection.x);
            Assert.AreEqual(0, intersection.y);
            Assert.AreEqual(0, intersection.width);
            Assert.AreEqual(0, intersection.height);

            intersection = rect2.Intersect(rect3);
            Assert.AreEqual(5, intersection.x);
            Assert.AreEqual(5, intersection.y);
            Assert.AreEqual(5, intersection.width);
            Assert.AreEqual(5, intersection.height);
        }

        [TestMethod]
        public void TestIntersectionOneInsideOther()
        {
            var rect1 = new RectInt(0, 0, 10, 10);
            var rect2 = new RectInt(5, 5, 5, 5);

            var intersection = rect1.Intersect(rect2);

            Assert.AreEqual(5, intersection.x);
            Assert.AreEqual(5, intersection.y);
            Assert.AreEqual(5, intersection.width);
            Assert.AreEqual(5, intersection.height);
        }

        [TestMethod]
        public void TestIntersectionOneInsideOtherNegative()
        {
            var rect1 = new RectInt(-10, -10, -10, -10);
            var rect2 = new RectInt(-5, -5, -5, -5);

            var intersection = rect1.Intersect(rect2);

            Assert.AreEqual(0, intersection.x);
            Assert.AreEqual(0, intersection.y);
            Assert.AreEqual(0, intersection.width);
            Assert.AreEqual(0, intersection.height);
        }

        [TestMethod]
        public void TestIntersectionOneInsideOtherOnePositiveOneNegative()
        {
            var rect1 = new RectInt(-10, -10, -10, -10);
            var rect2 = new RectInt(5, 5, 5, 5);
            var rect3 = new RectInt(-5, -5, 15, 15);

            var intersection = rect1.Intersect(rect2);
            Assert.AreEqual(0, intersection.x);
            Assert.AreEqual(0, intersection.y);
            Assert.AreEqual(0, intersection.width);
            Assert.AreEqual(0, intersection.height);

            intersection = rect2.Intersect(rect3);
            Assert.AreEqual(5, intersection.x);
            Assert.AreEqual(5, intersection.y);
            Assert.AreEqual(5, intersection.width);
            Assert.AreEqual(5, intersection.height);
        }

        [TestMethod]
        public void TestIntersectMixedCoordinatesTopLeft()
        {
            var rect1 = new RectInt(0, 0, 10, 10);
            var rect2 = new RectInt(-5, 5, 10, 10);

            var intersection = rect1.Intersect(rect2);

            Assert.AreEqual(0, intersection.x);
            Assert.AreEqual(5, intersection.y);
            Assert.AreEqual(5, intersection.width);
            Assert.AreEqual(5, intersection.height);
            
        }

        [TestMethod]
        public void TestIntersectMixedCoordinatesBottomLeft()
        {
            var rect1 = new RectInt(0, 0, 10, 10);
            var rect2 = new RectInt(-5, -5, 10, 10);

            var intersection = rect1.Intersect(rect2);

            Assert.AreEqual(0, intersection.x);
            Assert.AreEqual(0, intersection.y);
            Assert.AreEqual(5, intersection.width);
            Assert.AreEqual(5, intersection.height);
        }

        [TestMethod]
        public void TestIntersectMixedCoordinatesBottomRight()
        {
            var rect1 = new RectInt(0, 0, 10, 10);
            var rect2 = new RectInt(5, -5, 10, 10);

            var intersection = rect1.Intersect(rect2);

            Assert.AreEqual(5, intersection.x);
            Assert.AreEqual(0, intersection.y);
            Assert.AreEqual(5, intersection.width);
            Assert.AreEqual(5, intersection.height);
        }

        [TestMethod]
        public void TestIntersectMixedCoordinatesTopRight()
        {
            var rect1 = new RectInt(0, 0, 10, 10);
            var rect2 = new RectInt(5, 5, 10, 10);

            var intersection = rect1.Intersect(rect2);

            Assert.AreEqual(5, intersection.x);
            Assert.AreEqual(5, intersection.y);
            Assert.AreEqual(5, intersection.width);
            Assert.AreEqual(5, intersection.height);
        }

        [TestMethod]
        public void TestIntersectOnEdge()
        {
            var rect1 = new RectInt(0, 0, 10, 10);
            var rect2 = new RectInt(10, 10, 5, 5);

            var intersection = rect1.Intersect(rect2);

            Assert.AreEqual(0, intersection.x);
            Assert.AreEqual(0, intersection.y);
            Assert.AreEqual(0, intersection.width);
            Assert.AreEqual(0, intersection.height);
        }

        [TestMethod]
        public void TestOverlapsOnEdge()
        {
            var rect1 = new RectInt(0, 0, 10, 10);
            var rect2 = new RectInt(10, 10, 5, 5);

            Assert.IsFalse(rect1.Overlaps(rect2));
        }

        [TestMethod]
        public void TestIntersectOnEdge2()
        {
            var rect1 = new RectInt(0, 0, 10, 10);
            var rect2 = new RectInt(-3, -3, 3, 3);

            var intersection = rect1.Intersect(rect2);

            Assert.AreEqual(0, intersection.x);
            Assert.AreEqual(0, intersection.y);
            Assert.AreEqual(0, intersection.width);
            Assert.AreEqual(0, intersection.height);
        }

        [TestMethod]
        public void TestOverlapsOnEdge2()
        {
            var rect1 = new RectInt(0, 0, 10, 10);
            var rect2 = new RectInt(-3, -3, 3, 3);

            Assert.IsFalse(rect1.Overlaps(rect2));
        }

    }
}
