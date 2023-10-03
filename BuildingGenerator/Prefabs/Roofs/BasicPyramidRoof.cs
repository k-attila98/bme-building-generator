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

            //bottom face triangle 1 texturevertices
            TextureVertex[] textureVertices1 = new TextureVertex[3];
            textureVertices1[0] = new TextureVertex(new Vector2(0, 0));
            textureVertices1[1] = new TextureVertex(new Vector2(1, 0));
            textureVertices1[2] = new TextureVertex(new Vector2(1, 1));

            //bottom face triangle 2
            Vertex[] vertices2 = new Vertex[3];
            vertices2[0] = new Vertex(new Vector3(0, 0, 0));
            vertices2[1] = new Vertex(new Vector3(2, 0, 2));
            vertices2[2] = new Vertex(new Vector3(0, 0, 2));

            //bottom face triangle 2 texturevertices
            TextureVertex[] textureVertices2 = new TextureVertex[3];
            textureVertices2[0] = new TextureVertex(new Vector2(0, 0));
            textureVertices2[1] = new TextureVertex(new Vector2(1, 1));
            textureVertices2[2] = new TextureVertex(new Vector2(0, 1));

            //side face 1
            Vertex[] vertices3 = new Vertex[3];
            vertices3[0] = new Vertex(new Vector3(0, 0, 0));
            vertices3[1] = new Vertex(new Vector3(2, 0, 0));
            vertices3[2] = new Vertex(new Vector3(1, 1, 1));

            //side face 1 texturevertices
            TextureVertex[] textureVertices3 = new TextureVertex[3];
            textureVertices3[0] = new TextureVertex(new Vector2(0, 0));
            textureVertices3[1] = new TextureVertex(new Vector2(1, 0));
            textureVertices3[2] = new TextureVertex(new Vector2(0.5f, 1));

            //side face 2
            Vertex[] vertices4 = new Vertex[3];
            vertices4[0] = new Vertex(new Vector3(2, 0, 0));
            vertices4[1] = new Vertex(new Vector3(2, 0, 2));
            vertices4[2] = new Vertex(new Vector3(1, 1, 1));

            //side face 2 texturevertices
            TextureVertex[] textureVertices4 = new TextureVertex[3];
            textureVertices4[0] = new TextureVertex(new Vector2(0, 0));
            textureVertices4[1] = new TextureVertex(new Vector2(1, 0));
            textureVertices4[2] = new TextureVertex(new Vector2(0.5f, 1));

            //side face 3
            Vertex[] vertices5 = new Vertex[3];
            vertices5[0] = new Vertex(new Vector3(2, 0, 2));
            vertices5[1] = new Vertex(new Vector3(0, 0, 2));
            vertices5[2] = new Vertex(new Vector3(1, 1, 1));

            //side face 3 texturevertices
            TextureVertex[] textureVertices5 = new TextureVertex[3];
            textureVertices5[0] = new TextureVertex(new Vector2(0, 0));
            textureVertices5[1] = new TextureVertex(new Vector2(1, 0));
            textureVertices5[2] = new TextureVertex(new Vector2(0.5f, 1));

            //side face 4
            Vertex[] vertices6 = new Vertex[3];
            vertices6[0] = new Vertex(new Vector3(0, 0, 2));
            vertices6[1] = new Vertex(new Vector3(0, 0, 0));
            vertices6[2] = new Vertex(new Vector3(1, 1, 1));

            //side face 4 texturevertices
            TextureVertex[] textureVertices6 = new TextureVertex[3];
            textureVertices6[0] = new TextureVertex(new Vector2(0, 0));
            textureVertices6[1] = new TextureVertex(new Vector2(1, 0));
            textureVertices6[2] = new TextureVertex(new Vector2(0.5f, 1));

            Face[] faces = new Face[6];
            faces[0] = new Face(vertices1, textureVertices1);
            faces[1] = new Face(vertices2, textureVertices2);
            faces[2] = new Face(vertices3, textureVertices3);
            faces[3] = new Face(vertices4, textureVertices4);
            faces[4] = new Face(vertices5, textureVertices5);
            faces[5] = new Face(vertices6, textureVertices6);

            Transform prefab = new Transform("pyramid roof");
            prefab.Faces = faces;
            prefab.Width = 2;
            prefab.Height = 1;

            return prefab;
        }
    }
}
