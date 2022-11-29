using BuildingGenerator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.Prefabs.Roofs
{
    public class BasicPyramidRoof : IPrefab
    {
        public Transform GetTransform()
        {
            //bottom face triangle 1
            Vertex[] vertices1 = new Vertex[3];
            vertices1[0] = new Vertex(new Vector3(0, 0, 0));
            vertices1[1] = new Vertex(new Vector3(2, 0, 0));
            vertices1[2] = new Vertex(new Vector3(2, 0, 2));

            //bottom face triangle 2
            Vertex[] vertices2 = new Vertex[3];
            vertices2[0] = new Vertex(new Vector3(0, 0, 0));
            vertices2[1] = new Vertex(new Vector3(2, 0, 2));
            vertices2[2] = new Vertex(new Vector3(0, 0, 2));

            //side face 1
            Vertex[] vertices3 = new Vertex[3];
            vertices3[0] = new Vertex(new Vector3(0, 0, 0));
            vertices3[1] = new Vertex(new Vector3(2, 0, 0));
            vertices3[2] = new Vertex(new Vector3(1, 1, 1));

            //side face 2
            Vertex[] vertices4 = new Vertex[3];
            vertices4[0] = new Vertex(new Vector3(2, 0, 0));
            vertices4[1] = new Vertex(new Vector3(2, 0, 2));
            vertices4[2] = new Vertex(new Vector3(1, 1, 1));

            //side face 3
            Vertex[] vertices5 = new Vertex[3];
            vertices5[0] = new Vertex(new Vector3(2, 0, 2));
            vertices5[1] = new Vertex(new Vector3(0, 0, 2));
            vertices5[2] = new Vertex(new Vector3(1, 1, 1));

            //side face 4
            Vertex[] vertices6 = new Vertex[3];
            vertices6[0] = new Vertex(new Vector3(0, 0, 2));
            vertices6[1] = new Vertex(new Vector3(0, 0, 0));
            vertices6[2] = new Vertex(new Vector3(1, 1, 1));

            Face[] faces = new Face[6];
            faces[0] = new Face(vertices1);
            faces[1] = new Face(vertices2);
            faces[2] = new Face(vertices3);
            faces[3] = new Face(vertices4);
            faces[4] = new Face(vertices5);
            faces[5] = new Face(vertices6);

            Transform prefab = new Transform("pyramid roof");
            prefab.Faces = faces;
            prefab.Width = 2;
            prefab.Height = 1;

            return prefab;
        }
    }
}
