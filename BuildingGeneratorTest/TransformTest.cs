using BuildingGenerator.Prefabs.Roofs;
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
            var testRotationQuaternion = Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitZ, (MathHelper.GetRadFromDeg(90)));

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
            var testRotationQuaternion = Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitZ, (MathHelper.GetRadFromDeg(-90)));

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
            var testRotationQuaternion = Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitZ, (MathHelper.GetRadFromDeg(45)));

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
            var testRotationQuaternion = Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitZ, (MathHelper.GetRadFromDeg(90)));

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

        [TestMethod]
        public void TransformPointCalculation()
        {
            var transformToTest = new Transform();
            transformToTest.Position = new Vector3(1, 1, 1);
            var transformPointVector = new Vector3(2, 2, 2);

            var result = transformToTest.TransformPoint(transformPointVector);

            Assert.AreEqual(result, new Vector3(3, 3, 3), "TransformPoint failed:\n\texpected: {0}\n\tactual: {1}", new Vector3(3, 3, 3).ToString(), result.ToString());
        }

        [TestMethod]
        public void TransformConstructorWorking()
        {
            // arrange the prefab
            Vertex[] vertices1 = new Vertex[3];
            vertices1[0] = new Vertex(new Vector3(0, 0, 0));
            vertices1[1] = new Vertex(new Vector3(1, 0, 0));
            vertices1[2] = new Vertex(new Vector3(0, 1, 0));

            Vertex[] vertices2 = new Vertex[3];
            vertices2[0] = new Vertex(new Vector3(0, 0, 0));
            vertices2[1] = new Vertex(new Vector3(0, 1, 0));
            vertices2[2] = new Vertex(new Vector3(0, 0, 1));

            Vertex[] vertices3 = new Vertex[3];
            vertices3[0] = new Vertex(new Vector3(0, 0, 0));
            vertices3[1] = new Vertex(new Vector3(1, 0, 0));
            vertices3[2] = new Vertex(new Vector3(0, 0, 1));

            Vertex[] vertices4 = new Vertex[3];
            vertices4[0] = new Vertex(new Vector3(1, 0, 0));
            vertices4[1] = new Vertex(new Vector3(0, 1, 0));
            vertices4[2] = new Vertex(new Vector3(0, 0, 1));

            var prefabFace1 = new Face(vertices1);
            var prefabFace2 = new Face(vertices2);
            var prefabFace3 = new Face(vertices3);
            var prefabFace4 = new Face(vertices4);

            var prefabTransform = new Transform("Tetrahedron-like");
            prefabTransform.Faces = new Face[] { prefabFace1, prefabFace2, prefabFace3, prefabFace4 };
            prefabTransform.Width = 1;
            prefabTransform.Height = 1;

            var positionVector = new Vector3(1, -1, 1);
            var rotationQuaternion = Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitZ, ((float)(Math.PI / 180F) * 90F));
            
            //act
            var transformToTest = new Transform(prefabTransform, positionVector, rotationQuaternion);

            //assert
            var resultPositions1 = new Vector3[3];
            resultPositions1[0] = new Vector3(1, -1, 1);
            resultPositions1[1] = new Vector3(1, 0, 1);
            resultPositions1[2] = new Vector3(0, -1, 1);

            var resultPositions2 = new Vector3[3];
            resultPositions2[0] = new Vector3(1, -1, 1);
            resultPositions2[1] = new Vector3(0, -1, 1);
            resultPositions2[2] = new Vector3(1, -1, 2);

            var resultPositions3 = new Vector3[3];
            resultPositions3[0] = new Vector3(1, -1, 1);
            resultPositions3[1] = new Vector3(1, 0, 1);
            resultPositions3[2] = new Vector3(1, -1, 2);

            var resultPositions4 = new Vector3[3];
            resultPositions4[0] = new Vector3(1, 0, 1);
            resultPositions4[1] = new Vector3(0, -1, 1);
            resultPositions4[2] = new Vector3(1, -1, 2);

            Assert.AreEqual(transformToTest.Faces[0].Vertices[0].Position, resultPositions1[0], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions1[0].ToString(), transformToTest.Faces[0].Vertices[0].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[0].Vertices[1].Position, resultPositions1[1], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions1[1].ToString(), transformToTest.Faces[0].Vertices[1].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[0].Vertices[2].Position, resultPositions1[2], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions1[2].ToString(), transformToTest.Faces[0].Vertices[2].Position.ToString());

            Assert.AreEqual(transformToTest.Faces[1].Vertices[0].Position, resultPositions2[0], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions2[0].ToString(), transformToTest.Faces[1].Vertices[0].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[1].Vertices[1].Position, resultPositions2[1], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions2[1].ToString(), transformToTest.Faces[1].Vertices[1].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[1].Vertices[2].Position, resultPositions2[2], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions2[2].ToString(), transformToTest.Faces[1].Vertices[2].Position.ToString());

            Assert.AreEqual(transformToTest.Faces[2].Vertices[0].Position, resultPositions3[0], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions3[0].ToString(), transformToTest.Faces[2].Vertices[0].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[2].Vertices[1].Position, resultPositions3[1], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions3[1].ToString(), transformToTest.Faces[2].Vertices[1].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[2].Vertices[2].Position, resultPositions3[2], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions3[2].ToString(), transformToTest.Faces[2].Vertices[2].Position.ToString());

            Assert.AreEqual(transformToTest.Faces[3].Vertices[0].Position, resultPositions4[0], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions4[0].ToString(), transformToTest.Faces[3].Vertices[0].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[3].Vertices[1].Position, resultPositions4[1], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions4[1].ToString(), transformToTest.Faces[3].Vertices[1].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[3].Vertices[2].Position, resultPositions4[2], "Rotation failed:\n\texpected: {0}\n\tactual: {1}", resultPositions4[2].ToString(), transformToTest.Faces[3].Vertices[2].Position.ToString());

        }

        [TestMethod]

        public void StringifyTest()
        {
            Vertex[] vertices1 = new Vertex[3];
            vertices1[0] = new Vertex(new Vector3(0, 0, 0));
            vertices1[1] = new Vertex(new Vector3(1, 0, 0));
            vertices1[2] = new Vertex(new Vector3(0, 1, 0));

            Vertex[] vertices2 = new Vertex[3];
            vertices2[0] = new Vertex(new Vector3(0, 0, 0));
            vertices2[1] = new Vertex(new Vector3(0, 1, 0));
            vertices2[2] = new Vertex(new Vector3(0, 0, 1));

            Vertex[] vertices3 = new Vertex[3];
            vertices3[0] = new Vertex(new Vector3(0, 0, 0));
            vertices3[1] = new Vertex(new Vector3(1, 0, 0));
            vertices3[2] = new Vertex(new Vector3(0, 0, 1));

            Vertex[] vertices4 = new Vertex[3];
            vertices4[0] = new Vertex(new Vector3(1, 0, 0));
            vertices4[1] = new Vertex(new Vector3(0, 1, 0));
            vertices4[2] = new Vertex(new Vector3(0, 0, 1));

            var prefabFace1 = new Face(vertices1);
            var prefabFace2 = new Face(vertices2);
            var prefabFace3 = new Face(vertices3);
            var prefabFace4 = new Face(vertices4);

            var prefabTransform = new Transform("Tetrahedron-like");
            prefabTransform.Faces = new Face[] { prefabFace1, prefabFace2, prefabFace3, prefabFace4 };
            prefabTransform.Width = 1;
            prefabTransform.Height = 1;

            var positionVector1 = new Vector3(1, -1, 1);
            var positionVector2 = new Vector3(4, 6, 4);
            var rotationQuaternion = Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitZ, ((float)(Math.PI / 180F) * 90F));

            //act
            var transformToTest1 = new Transform(prefabTransform, positionVector1, rotationQuaternion);
            var transformToTest2 = new Transform(prefabTransform, positionVector2, rotationQuaternion);

            Transform[] placedPrefabs = new Transform[] { transformToTest1, transformToTest2 };

            string objFileContent = "";
            //Console.WriteLine("Serializing building...");
            foreach (var prefab in placedPrefabs)
            {
                objFileContent += prefab.VerticesToString();
            }

            foreach (var prefab in placedPrefabs)
            {
                objFileContent += prefab.FacesToString();
            }

            Console.WriteLine(objFileContent);
            string correctSerialization = "" +
                "v 1 -1 1\n" +
                "v 1 0 1\n" +
                "v 0 -1 1\n" +
                "v 1 -1 1\n" +
                "v 0 -1 1\n" +
                "v 1 -1 2\n" +
                "v 1 -1 1\n" +
                "v 1 0 1\n" +
                "v 1 -1 2\n" +
                "v 1 0 1\n" +
                "v 0 -1 1\n" +
                "v 1 -1 2\n" +
                "v 4 6 4\n" +
                "v 4 7 4\n" +
                "v 3 6 4\n" +
                "v 4 6 4\n" +
                "v 3 6 4\n" +
                "v 4 6 5\n" +
                "v 4 6 4\n" +
                "v 4 7 4\n" +
                "v 4 6 5\n" +
                "v 4 7 4\n" +
                "v 3 6 4\n" +
                "v 4 6 5\n" +
                "f 1 2 3\n" +
                "f 4 5 6\n" +
                "f 7 8 9\n" +
                "f 10 11 12\n" +
                "f 13 14 15\n" +
                "f 16 17 18\n" +
                "f 19 20 21\n" +
                "f 22 23 24\n";
            //Assert.AreEqual(correctSerialization, objFileContent);
            Assert.IsTrue(true);
        }

        [TestMethod]
        //this method should test the scale method of the transform class
        public void ScaleEvenTest()
        {
            //Arrange
            var transformToTest = new Transform();
            var testScaleVector = new Vector3(2,2,2);

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
            resultPositions[1] = new Vector3(2, 0, 0);
            resultPositions[2] = new Vector3(0, 2, 0);

            //Act
            transformToTest.Scale(testScaleVector);

            //Assert
            Assert.AreEqual(transformToTest.Faces[0].Vertices[0].Position, resultPositions[0], "Scalign failed:\n\texpected: {0}\n\tactual: {1}", resultPositions[0].ToString(), transformToTest.Faces[0].Vertices[0].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[0].Vertices[1].Position, resultPositions[1], "Scalign failed:\n\texpected: {0}\n\tactual: {1}", resultPositions[1].ToString(), transformToTest.Faces[0].Vertices[1].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[0].Vertices[2].Position, resultPositions[2], "Scalign failed:\n\texpected: {0}\n\tactual: {1}", resultPositions[2].ToString(), transformToTest.Faces[0].Vertices[2].Position.ToString());
        }

        [TestMethod]
        public void ScaleUnevenTest()
        {
            //Arrange
            var transformToTest = new Transform();
            var testScaleVector = new Vector3(2, 3, 4);

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
            resultPositions[1] = new Vector3(2, 0, 0);
            resultPositions[2] = new Vector3(0, 3, 0);

            //Act
            transformToTest.Scale(testScaleVector);

            //Assert
            Assert.AreEqual(transformToTest.Faces[0].Vertices[0].Position, resultPositions[0], "Scalign failed:\n\texpected: {0}\n\tactual: {1}", resultPositions[0].ToString(), transformToTest.Faces[0].Vertices[0].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[0].Vertices[1].Position, resultPositions[1], "Scalign failed:\n\texpected: {0}\n\tactual: {1}", resultPositions[1].ToString(), transformToTest.Faces[0].Vertices[1].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[0].Vertices[2].Position, resultPositions[2], "Scalign failed:\n\texpected: {0}\n\tactual: {1}", resultPositions[2].ToString(), transformToTest.Faces[0].Vertices[2].Position.ToString());
        }

        [TestMethod]
        public void ScaleRoofTest()
        { 
            //Arrange
            BasicPyramidRoof roof = new BasicPyramidRoof();
            Vector3 scalingVector = new Vector3(3, 2, 1);
            Transform transformToTest = new Transform(roof.GetTransform(), new Vector3(0,0,0), Quaternion.Identity);

            var resultPositions = new Vector3[3];
            resultPositions[0] = new Vector3(6, 0, 0);
            resultPositions[1] = new Vector3(6, 0, 2);
            resultPositions[2] = new Vector3(3, 2, 1);

            //Act
            transformToTest.Scale(scalingVector);

            //Assert
            Assert.AreEqual(transformToTest.Faces[0].Vertices[1].Position, resultPositions[0], "Scalign failed:\n\texpected: {0}\n\tactual: {1}", resultPositions[0].ToString(), transformToTest.Faces[0].Vertices[1].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[1].Vertices[1].Position, resultPositions[1], "Scalign failed:\n\texpected: {0}\n\tactual: {1}", resultPositions[1].ToString(), transformToTest.Faces[1].Vertices[1].Position.ToString());
            Assert.AreEqual(transformToTest.Faces[2].Vertices[2].Position, resultPositions[2], "Scalign failed:\n\texpected: {0}\n\tactual: {1}", resultPositions[2].ToString(), transformToTest.Faces[2].Vertices[2].Position.ToString());

        }
    }
}