using BuildingGenerator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.Prefabs.Floors
{
    public class BasicFloor : IPrefab
    {
        public Transform GetTransform()
        {
            //face 1 triangle 1
            Vertex[] vertices1 = new Vertex[3];
            vertices1[0] = new Vertex(new Vector3(0, -0.05f, 0));
            vertices1[1] = new Vertex(new Vector3(2, -0.05f, 0));
            vertices1[2] = new Vertex(new Vector3(2, 0.05f, 0));

            //face 1 triangle 2
            Vertex[] vertices2 = new Vertex[3];
            vertices2[0] = new Vertex(new Vector3(0, -0.05f, 0));
            vertices2[1] = new Vertex(new Vector3(2, 0.05f, 0));
            vertices2[2] = new Vertex(new Vector3(0, 0.05f, 0));

            //face 2 triangle 1
            Vertex[] vertices3 = new Vertex[3];
            vertices3[0] = new Vertex(new Vector3(2, -0.05f, 2));
            vertices3[1] = new Vertex(new Vector3(0, -0.05f, 2));
            vertices3[2] = new Vertex(new Vector3(0, 0.05f, 2));

            //face 2 triangle 2
            Vertex[] vertices4 = new Vertex[3];
            vertices4[0] = new Vertex(new Vector3(2, -0.05f, 2));
            vertices4[1] = new Vertex(new Vector3(0, 0.05f, 2));
            vertices4[2] = new Vertex(new Vector3(2, 0.05f, 2));

            //side face 1 triangle 1
            Vertex[] vertices5 = new Vertex[3];
            vertices5[0] = new Vertex(new Vector3(0, -0.05f, 2));
            vertices5[1] = new Vertex(new Vector3(0, -0.05f, 0));
            vertices5[2] = new Vertex(new Vector3(0, 0.05f, 0));

            //side face 1 triangle 2
            Vertex[] vertices6 = new Vertex[3];
            vertices6[0] = new Vertex(new Vector3(0, -0.05f, 2));
            vertices6[1] = new Vertex(new Vector3(0, 0.05f, 0));
            vertices6[2] = new Vertex(new Vector3(0, 0.05f, 2));

            //side face 2 triangle 1
            Vertex[] vertices7 = new Vertex[3];
            vertices7[0] = new Vertex(new Vector3(2, -0.05f, 0));
            vertices7[1] = new Vertex(new Vector3(2, -0.05f, 2));
            vertices7[2] = new Vertex(new Vector3(2, 0.05f, 2));

            //side face 2 triangle 2
            Vertex[] vertices8 = new Vertex[3];
            vertices8[0] = new Vertex(new Vector3(2, -0.05f, 0));
            vertices8[1] = new Vertex(new Vector3(2, 0.05f, 2));
            vertices8[2] = new Vertex(new Vector3(2, 0.05f, 0));

            //top face 1 triangle 1
            Vertex[] vertices9 = new Vertex[3];
            vertices9[0] = new Vertex(new Vector3(0, 0.05f, 0));
            vertices9[1] = new Vertex(new Vector3(2, 0.05f, 0));
            vertices9[2] = new Vertex(new Vector3(2, 0.05f, 2));

            //top face 1 triangle 2
            Vertex[] vertices10 = new Vertex[3];
            vertices10[0] = new Vertex(new Vector3(0, 0.05f, 0));
            vertices10[1] = new Vertex(new Vector3(2, 0.05f, 2));
            vertices10[2] = new Vertex(new Vector3(0, 0.05f, 2));

            //bottom face 1 triangle 1
            Vertex[] vertices11 = new Vertex[3];
            vertices11[0] = new Vertex(new Vector3(0, -0.05f, 2));
            vertices11[1] = new Vertex(new Vector3(2, -0.05f, 2));
            vertices11[2] = new Vertex(new Vector3(2, -0.05f, 0));

            //bottom face 1 triangle 2
            Vertex[] vertices12 = new Vertex[3];
            vertices12[0] = new Vertex(new Vector3(0, -0.05f, 2));
            vertices12[1] = new Vertex(new Vector3(2, -0.05f, 0));
            vertices12[2] = new Vertex(new Vector3(0, -0.05f, 0));

            Face[] faces = new Face[12];
            faces[0] = new Face(vertices1);
            faces[1] = new Face(vertices2);
            faces[2] = new Face(vertices3);
            faces[3] = new Face(vertices4);
            faces[4] = new Face(vertices5);
            faces[5] = new Face(vertices6);
            faces[6] = new Face(vertices7);
            faces[7] = new Face(vertices8);
            faces[8] = new Face(vertices9);
            faces[9] = new Face(vertices10);
            faces[10] = new Face(vertices11);
            faces[11] = new Face(vertices12);

            Transform prefab = new Transform("basic floor");
            prefab.Faces = faces;
            prefab.Width = 2;
            prefab.Height = 0;
            return prefab;
        }
    }
}
