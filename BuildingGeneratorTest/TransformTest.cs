using BuildingGenerator.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;
using Vector3 = BuildingGenerator.Shared.Vector3;

namespace BuildingGeneratorTest
{
    [TestClass]
    public class TransformTest
    {
        [TestMethod]
        public void AllPositiveTranslate()
        {
            //Arrange
            var transformToTest = new Transform();
            Vector3 testTranslationVector = new Vector3(1, 1, 1);

            //Act
            transformToTest.Translate(testTranslationVector);

            //Assert
            Assert.AreEqual(transformToTest.Position, testTranslationVector, "Translation failed:\n\texpected: {0}\n\tactual: {1}", testTranslationVector.ToString(), transformToTest.Position.ToString());
        }

        [TestMethod]
        public void MixedTranslate()
        {
            //Arrange
            var transformToTest = new Transform();
            Vector3 testTranslationVector = new Vector3(1, -1, -1);

            //Act
            transformToTest.Translate(testTranslationVector);

            //Assert
            Assert.AreEqual(transformToTest.Position, testTranslationVector, "Translation failed:\n\texpected: {0}\n\tactual: {1}", testTranslationVector.ToString(), transformToTest.Position.ToString());
        }

        [TestMethod]
        public void AllNegativeTranslate()
        {
            //Arrange
            var transformToTest = new Transform();
            Vector3 testTranslationVector = new Vector3(-1, -1, -1);

            //Act
            transformToTest.Translate(testTranslationVector);

            //Assert
            Assert.AreEqual(transformToTest.Position, testTranslationVector, "Translation failed:\n\texpected: {0}\n\tactual: {1}", testTranslationVector.ToString(), transformToTest.Position.ToString());
        }

        [TestMethod]
        public void AllPositiveRotate()
        {
            //Arrange
            var transformToTest = new Transform();
            var testRotationQuaternion = Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitZ, ((float)(Math.PI / 180F) * 90F));

            Vertex[] vertices = new Vertex[3];
            vertices[0] = new Vertex(new Vector3(0, 0, 0));
            vertices[1] = new Vertex(new Vector3(1, 0, 0));
            vertices[2] = new Vertex(new Vector3(0, 1, 0));

            Face[] faces = new Face[1];
            faces[0] = new Face(vertices);
            //faces[0].Vertices = ;

            transformToTest.Faces = faces;

            var resultPositions = new Vector3[3];
            resultPositions[0] = new Vector3(0, 0, 0);
            resultPositions[1] = new Vector3(0, 1, 0);
            resultPositions[2] = new Vector3(-1, 0, 0);

            //Act
            transformToTest.Rotate(testRotationQuaternion);

            //Assert
            Assert.AreEqual(transformToTest.Faces[0].Vertices[0].Position, resultPositions[0], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions[0].ToString(), transformToTest.Faces[0].Vertices[0].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[0].Vertices[1].Position, resultPositions[1], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions[1].ToString(), transformToTest.Faces[0].Vertices[1].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[0].Vertices[2].Position, resultPositions[2], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions[2].ToString(), transformToTest.Faces[0].Vertices[2].Position.ToString());
        }

        [TestMethod]
        public void AllNegativeRotate()
        {
            //Arrange
            var transformToTest = new Transform();
            var testRotationQuaternion = Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitZ, ((float)(Math.PI / 180F) * -90F));

            Vertex[] vertices = new Vertex[3];
            vertices[0] = new Vertex(new Vector3(0, 0, 0));
            vertices[1] = new Vertex(new Vector3(1, 0, 0));
            vertices[2] = new Vertex(new Vector3(0, 1, 0));

            Face[] faces = new Face[1];
            faces[0] = new Face(vertices);
            //faces[0].Vertices = ;

            transformToTest.Faces = faces;

            var resultPositions = new Vector3[3];
            resultPositions[0] = new Vector3(0, 0, 0);
            resultPositions[1] = new Vector3(0, -1, 0);
            resultPositions[2] = new Vector3(1, 0, 0);

            //Act
            transformToTest.Rotate(testRotationQuaternion);

            //Assert
            Assert.AreEqual(transformToTest.Faces[0].Vertices[0].Position, resultPositions[0], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions[0].ToString(), transformToTest.Faces[0].Vertices[0].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[0].Vertices[1].Position, resultPositions[1], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions[1].ToString(), transformToTest.Faces[0].Vertices[1].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[0].Vertices[2].Position, resultPositions[2], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions[2].ToString(), transformToTest.Faces[0].Vertices[2].Position.ToString());
        }

        [TestMethod]
        public void MixedRotate()
        {
            //Arrange
            var transformToTest = new Transform();
            var testRotationQuaternion = Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitZ, ((float)(Math.PI / 180F) * 45F));

            Vertex[] vertices = new Vertex[3];
            vertices[0] = new Vertex(new Vector3(0, 0, 0));
            vertices[1] = new Vertex(new Vector3(1, 0, 0));
            vertices[2] = new Vertex(new Vector3(0, 1, 0));

            Face[] faces = new Face[1];
            faces[0] = new Face(vertices);
            //faces[0].Vertices = ;

            transformToTest.Faces = faces;

            var resultPositions = new Vector3[3];
            resultPositions[0] = new Vector3(0, 0, 0);
            resultPositions[1] = new Vector3(0.707F, 0.707F, 0);
            resultPositions[2] = new Vector3(-0.707F, 0.707F, 0);

            //Act
            transformToTest.Rotate(testRotationQuaternion);

            //Assert
            Assert.AreEqual(transformToTest.Faces[0].Vertices[0].Position, resultPositions[0], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions[0].ToString(), transformToTest.Faces[0].Vertices[0].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[0].Vertices[1].Position, resultPositions[1], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions[1].ToString(), transformToTest.Faces[0].Vertices[1].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[0].Vertices[2].Position, resultPositions[2], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions[2].ToString(), transformToTest.Faces[0].Vertices[2].Position.ToString());
        }

        [TestMethod]
        public void NormalCalculation()
        {
            //Arrange
            var transformToTest = new Transform();
            //var testRotationQuaternion = Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitZ, ((float)(Math.PI / 180F) * 45F));

            Vertex[] vertices = new Vertex[3];
            vertices[0] = new Vertex(new Vector3(0, 0, 0));
            vertices[1] = new Vertex(new Vector3(1, 0, 0));
            vertices[2] = new Vertex(new Vector3(0, 1, 0));

            Face[] faces = new Face[1];
            faces[0] = new Face(vertices);

            transformToTest.Faces = faces;

            var resultNormal = new Vector3(0, 0, 1);

            //Act
            //transformToTest.Rotate(testRotationQuaternion);

            //Assert
            Assert.AreEqual(transformToTest.Faces[0].Normal, resultNormal, "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultNormal.ToString(), transformToTest.Faces[0].Normal.ToString());
        }

        [TestMethod]
        //translate transform with multiple faces
        public void TranslateMultipleFaces()
        {
            //Arrange
            var transformToTest = new Transform();
            var testTranslation = new Vector3(1, 1, 1);

            Vertex[] vertices1 = new Vertex[3];
            vertices1[0] = new Vertex(new Vector3(0, 0, 0));
            vertices1[1] = new Vertex(new Vector3(1, 0, 0));
            vertices1[2] = new Vertex(new Vector3(0, 1, 0));

            Vertex[] vertices2 = new Vertex[3];
            vertices2[0] = new Vertex(new Vector3(0, 0, 0));
            vertices2[1] = new Vertex(new Vector3(0, 1, 0));
            vertices2[2] = new Vertex(new Vector3(0, 0, 1));

            Face[] faces = new Face[2];
            faces[0] = new Face(vertices1);
            faces[1] = new Face(vertices2);

            transformToTest.Faces = faces;

            var resultPositions1 = new Vector3[3];
            resultPositions1[0] = new Vector3(1, 1, 1);
            resultPositions1[1] = new Vector3(2, 1, 1);
            resultPositions1[2] = new Vector3(1, 2, 1);

            var resultPositions2 = new Vector3[3];
            resultPositions2[0] = new Vector3(1, 1, 1);
            resultPositions2[1] = new Vector3(1, 2, 1);
            resultPositions2[2] = new Vector3(1, 1, 2);

            //Act
            transformToTest.Translate(testTranslation);

            //Assert
            Assert.AreEqual(transformToTest.Faces[0].Vertices[0].Position, resultPositions1[0], "Translation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions1[0].ToString(), transformToTest.Faces[0].Vertices[0].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[0].Vertices[1].Position, resultPositions1[1], "Translation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions1[1].ToString(), transformToTest.Faces[0].Vertices[1].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[0].Vertices[2].Position, resultPositions1[2], "Translation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions1[2].ToString(), transformToTest.Faces[0].Vertices[2].Position.ToString());

            Assert.AreEqual(transformToTest.Faces[1].Vertices[0].Position, resultPositions2[0], "Translation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions2[0].ToString(), transformToTest.Faces[1].Vertices[0].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[1].Vertices[1].Position, resultPositions2[1], "Translation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions2[1].ToString(), transformToTest.Faces[1].Vertices[1].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[1].Vertices[2].Position, resultPositions2[2], "Translation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions2[2].ToString(), transformToTest.Faces[1].Vertices[2].Position.ToString());
        }

        [TestMethod]
        //rotate transform with multiple faces
        public void RotateMultipleFaces()
        {
            //Arrange
            var transformToTest = new Transform();
            var testRotationQuaternion = Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitZ, ((float)(Math.PI / 180F) * 90F));

            Vertex[] vertices1 = new Vertex[3];
            vertices1[0] = new Vertex(new Vector3(0, 0, 0));
            vertices1[1] = new Vertex(new Vector3(1, 0, 0));
            vertices1[2] = new Vertex(new Vector3(0, 1, 0));

            Vertex[] vertices2 = new Vertex[3];
            vertices2[0] = new Vertex(new Vector3(0, 0, 0));
            vertices2[1] = new Vertex(new Vector3(0, 1, 0));
            vertices2[2] = new Vertex(new Vector3(0, 0, 1));

            Face[] faces = new Face[2];
            faces[0] = new Face(vertices1);
            faces[1] = new Face(vertices2);

            transformToTest.Faces = faces;

            var resultPositions1 = new Vector3[3];
            resultPositions1[0] = new Vector3(0, 0, 0);
            resultPositions1[1] = new Vector3(0, 1, 0);
            resultPositions1[2] = new Vector3(-1, 0, 0);

            var resultPositions2 = new Vector3[3];
            resultPositions2[0] = new Vector3(0, 0, 0);
            resultPositions2[1] = new Vector3(-1, 0, 0);
            resultPositions2[2] = new Vector3(0, 0, 1);

            //Act
            transformToTest.Rotate(testRotationQuaternion);

            //Assert
            Assert.AreEqual(transformToTest.Faces[0].Vertices[0].Position, resultPositions1[0], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions1[0].ToString(), transformToTest.Faces[0].Vertices[0].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[0].Vertices[1].Position, resultPositions1[1], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions1[1].ToString(), transformToTest.Faces[0].Vertices[1].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[0].Vertices[2].Position, resultPositions1[2], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions1[2].ToString(), transformToTest.Faces[0].Vertices[2].Position.ToString());

            Assert.AreEqual(transformToTest.Faces[1].Vertices[0].Position, resultPositions2[0], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions2[0].ToString(), transformToTest.Faces[1].Vertices[0].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[1].Vertices[1].Position, resultPositions2[1], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions2[1].ToString(), transformToTest.Faces[1].Vertices[1].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[1].Vertices[2].Position, resultPositions2[2], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions2[2].ToString(), transformToTest.Faces[1].Vertices[2].Position.ToString());
        }
    }
}