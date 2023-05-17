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

        [TestMethod]
        public void TestContainsWithEdgesZeroWidth()
        {
            var rect1 = new RectInt(3, 3, 0, 10);

            Assert.IsTrue(rect1.ContainsWithEdges(3,4));
        }

        [TestMethod]
        public void TestSubtractBottomRight()
        {
            var rect1 = new RectInt(0, 0, 4, 3);
            var rect2 = new RectInt(3, -2, 4, 3);

            var bounds = rect1.SubtractAndDivide(rect2);

            Assert.AreEqual(2, bounds.Length);

            Assert.AreEqual(bounds[0].width, 3);
            Assert.AreEqual(bounds[0].height, 3);
            Assert.AreEqual(bounds[0].x, 0);
            Assert.AreEqual(bounds[0].y, 0);

            Assert.AreEqual(bounds[1].width, 1);
            Assert.AreEqual(bounds[1].height, 2);
            Assert.AreEqual(bounds[1].x, 3);
            Assert.AreEqual(bounds[1].y, 1);

        }

        [TestMethod]
        public void TestSubtractBottomEdge()
        {
            var rect1 = new RectInt(0, 0, 4, 2);
            var rect2 = new RectInt(1, -1, 2, 2);

            var bounds = rect1.SubtractAndDivide(rect2);

            Assert.AreEqual(3, bounds.Length);

            Assert.AreEqual(1, bounds[0].width);
            Assert.AreEqual(2, bounds[0].height);
            Assert.AreEqual(0, bounds[0].x);
            Assert.AreEqual(0, bounds[0].y);

            Assert.AreEqual(2, bounds[1].width);
            Assert.AreEqual(1, bounds[1].height);
            Assert.AreEqual(1, bounds[1].x);
            Assert.AreEqual(1, bounds[1].y);

            Assert.AreEqual(1, bounds[2].width);
            Assert.AreEqual(2, bounds[2].height);
            Assert.AreEqual(3, bounds[2].x);
            Assert.AreEqual(0, bounds[2].y);
        }

        [TestMethod]
        public void TestSubtractLeftEdge()
        {
            var rect1 = new RectInt(0, 0, 4, 3);
            var rect2 = new RectInt(-1, 1, 2, 1);

            var bounds = rect1.SubtractAndDivide(rect2);

            Assert.AreEqual(3, bounds.Length);

            Assert.AreEqual(4, bounds[0].width);
            Assert.AreEqual(1, bounds[0].height);
            Assert.AreEqual(0, bounds[0].x);
            Assert.AreEqual(0, bounds[0].y);

            Assert.AreEqual(3, bounds[1].width);
            Assert.AreEqual(1, bounds[1].height);
            Assert.AreEqual(1, bounds[1].x);
            Assert.AreEqual(1, bounds[1].y);

            Assert.AreEqual(4, bounds[2].width);
            Assert.AreEqual(1, bounds[2].height);
            Assert.AreEqual(0, bounds[2].x);
            Assert.AreEqual(2, bounds[2].y);
        }

        [TestMethod]
        public void TestSubtractFullyEnclosed()
        {
            var rect1 = new RectInt(0, 0, 4, 3);
            var rect2 = new RectInt(2, 1, 1, 1);

            var bounds = rect1.SubtractAndDivide(rect2);

            Assert.AreEqual(4, bounds.Length);

            Assert.AreEqual(4, bounds[0].width);
            Assert.AreEqual(1, bounds[0].height);
            Assert.AreEqual(0, bounds[0].x);
            Assert.AreEqual(0, bounds[0].y);

            Assert.AreEqual(1, bounds[1].width);
            Assert.AreEqual(1, bounds[1].height);
            Assert.AreEqual(3, bounds[1].x);
            Assert.AreEqual(1, bounds[1].y);

            Assert.AreEqual(4, bounds[2].width);
            Assert.AreEqual(1, bounds[2].height);
            Assert.AreEqual(0, bounds[2].x);
            Assert.AreEqual(2, bounds[2].y);

            Assert.AreEqual(2, bounds[3].width);
            Assert.AreEqual(1, bounds[3].height);
            Assert.AreEqual(0, bounds[3].x);
            Assert.AreEqual(1, bounds[3].y);


        }

        // TODO: még teszteseteket írni a subtractanddividera
        [TestMethod]

        public void TestSubtractOnBottomLeftCorner()
        {
            var rect1 = new RectInt(0, 0, 4, 3);
            var rect2 = new RectInt(0, 0, 1, 2);

            var bounds = rect1.SubtractAndDivide(rect2);

            Assert.AreEqual(2, bounds.Length);

            Assert.AreEqual(3, bounds[0].width);
            Assert.AreEqual(3, bounds[0].height);
            Assert.AreEqual(1, bounds[0].x);
            Assert.AreEqual(0, bounds[0].y);

            Assert.AreEqual(1, bounds[1].width);
            Assert.AreEqual(1, bounds[1].height);
            Assert.AreEqual(0, bounds[1].x);
            Assert.AreEqual(2, bounds[1].y);


        }

        [TestMethod]
        public void TestSubtractOnBottomRightCorner()
        {
            var rect1 = new RectInt(0, 0, 4, 3);
            var rect2 = new RectInt(2, 0, 2, 2);

            var bounds = rect1.SubtractAndDivide(rect2);

            Assert.AreEqual(2, bounds.Length);

            Assert.AreEqual(2, bounds[0].width);
            Assert.AreEqual(3, bounds[0].height);
            Assert.AreEqual(0, bounds[0].x);
            Assert.AreEqual(0, bounds[0].y);

            Assert.AreEqual(2, bounds[1].width);
            Assert.AreEqual(1, bounds[1].height);
            Assert.AreEqual(2, bounds[1].x);
            Assert.AreEqual(2, bounds[1].y);


        }

        [TestMethod]
        public void TestSubtractOnTopRightCorner()
        {
            var rect1 = new RectInt(0, 0, 4, 3);
            var rect2 = new RectInt(2, 2, 2, 1);

            var bounds = rect1.SubtractAndDivide(rect2);

            Assert.AreEqual(2, bounds.Length);

            Assert.AreEqual(2, bounds[0].width);
            Assert.AreEqual(3, bounds[0].height);
            Assert.AreEqual(0, bounds[0].x);
            Assert.AreEqual(0, bounds[0].y);

            Assert.AreEqual(2, bounds[1].width);
            Assert.AreEqual(2, bounds[1].height);
            Assert.AreEqual(2, bounds[1].x);
            Assert.AreEqual(0, bounds[1].y);


        }

        [TestMethod]
        public void TestSubtractOnTopLeftCorner()
        {
            var rect1 = new RectInt(0, 0, 4, 3);
            var rect2 = new RectInt(0, 2, 3, 1);

            var bounds = rect1.SubtractAndDivide(rect2);

            Assert.AreEqual(2, bounds.Length);

            Assert.AreEqual(1, bounds[0].width);
            Assert.AreEqual(3, bounds[0].height);
            Assert.AreEqual(3, bounds[0].x);
            Assert.AreEqual(0, bounds[0].y);

            Assert.AreEqual(3, bounds[1].width);
            Assert.AreEqual(2, bounds[1].height);
            Assert.AreEqual(0, bounds[1].x);
            Assert.AreEqual(0, bounds[1].y);


        }

    }
}
