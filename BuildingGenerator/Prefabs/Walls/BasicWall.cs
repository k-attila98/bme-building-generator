using BuildingGenerator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.Prefabs.Walls
{
    public class BasicWall : IPrefab
    {
        public Transform GetTransform()
        {
            //face 1 triangle 1
            Vertex[] vertices1 = new Vertex[3];
            vertices1[0] = new Vertex(new Vector3(0, 0, -0.04f));
            vertices1[1] = new Vertex(new Vector3(2, 0, -0.04f));
            vertices1[2] = new Vertex(new Vector3(2, 4, -0.04f));

            //face 1 triangle 1 textrurevertices
            TextureVertex[] textureVertices1 = new TextureVertex[3];
            textureVertices1[0] = new TextureVertex(new Vector2(0, 0));
            textureVertices1[1] = new TextureVertex(new Vector2(1, 0));
            textureVertices1[2] = new TextureVertex(new Vector2(1, 1));

            //face 1 triangle 2
            Vertex[] vertices2 = new Vertex[3];
            vertices2[0] = new Vertex(new Vector3(0, 0, -0.04f));
            vertices2[1] = new Vertex(new Vector3(2, 4, -0.04f));
            vertices2[2] = new Vertex(new Vector3(0, 4, -0.04f));

            //face 1 triangle 2 texturevertices
            TextureVertex[] textureVertices2 = new TextureVertex[3];
            textureVertices2[0] = new TextureVertex(new Vector2(0, 0));
            textureVertices2[1] = new TextureVertex(new Vector2(1, 1));
            textureVertices2[2] = new TextureVertex(new Vector2(0, 1));

            //face 2 triangle 1
            Vertex[] vertices3 = new Vertex[3];
            vertices3[0] = new Vertex(new Vector3(2, 0, 0.04f));
            vertices3[1] = new Vertex(new Vector3(0, 0, 0.04f));
            vertices3[2] = new Vertex(new Vector3(0, 4, 0.04f));

            //face 2 triangle 1 texturevertices
            TextureVertex[] textureVertices3 = new TextureVertex[3];
            textureVertices3[0] = new TextureVertex(new Vector2(0, 0));
            textureVertices3[1] = new TextureVertex(new Vector2(1, 0));
            textureVertices3[2] = new TextureVertex(new Vector2(1, 1));

            //face 2 triangle 2
            Vertex[] vertices4 = new Vertex[3];
            vertices4[0] = new Vertex(new Vector3(2, 0, 0.04f));
            vertices4[1] = new Vertex(new Vector3(0, 4, 0.04f));
            vertices4[2] = new Vertex(new Vector3(2, 4, 0.04f));

            //face 2 triangle 2 texturevertices
            TextureVertex[] textureVertices4 = new TextureVertex[3];
            textureVertices4[0] = new TextureVertex(new Vector2(0, 0));
            textureVertices4[1] = new TextureVertex(new Vector2(1, 1));
            textureVertices4[2] = new TextureVertex(new Vector2(0, 1));


            //side face 1 triangle 1
            Vertex[] vertices5 = new Vertex[3];
            vertices5[0] = new Vertex(new Vector3(0, 0, 0.04f));
            vertices5[1] = new Vertex(new Vector3(0, 0, -0.04f));
            vertices5[2] = new Vertex(new Vector3(0, 4, -0.04f));

            //side face 1 triangle 1 texturevertices
            TextureVertex[] textureVertices5 = new TextureVertex[3];
            textureVertices5[0] = new TextureVertex(new Vector2(0, 0));
            textureVertices5[1] = new TextureVertex(new Vector2(1, 0));
            textureVertices5[2] = new TextureVertex(new Vector2(1, 1));

            //side face 1 triangle 2
            Vertex[] vertices6 = new Vertex[3];
            vertices6[0] = new Vertex(new Vector3(0, 0, 0.04f));
            vertices6[1] = new Vertex(new Vector3(0, 4, -0.04f));
            vertices6[2] = new Vertex(new Vector3(0, 4, 0.04f));

            //side face 1 triangle 2 texturevertices
            TextureVertex[] textureVertices6 = new TextureVertex[3];
            textureVertices6[0] = new TextureVertex(new Vector2(0, 0));
            textureVertices6[1] = new TextureVertex(new Vector2(1, 1));
            textureVertices6[2] = new TextureVertex(new Vector2(0, 1));

            //side face 2 triangle 1
            Vertex[] vertices7 = new Vertex[3];
            vertices7[0] = new Vertex(new Vector3(2, 0, -0.04f));
            vertices7[1] = new Vertex(new Vector3(2, 0, 0.04f));
            vertices7[2] = new Vertex(new Vector3(2, 4, 0.04f));

            //side face 2 triangle 1 texturevertices
            TextureVertex[] textureVertices7 = new TextureVertex[3];
            textureVertices7[0] = new TextureVertex(new Vector2(0, 0));
            textureVertices7[1] = new TextureVertex(new Vector2(1, 0));
            textureVertices7[2] = new TextureVertex(new Vector2(1, 1));

            //side face 2 triangle 2
            Vertex[] vertices8 = new Vertex[3];
            vertices8[0] = new Vertex(new Vector3(2, 0, -0.04f));
            vertices8[1] = new Vertex(new Vector3(2, 4, 0.04f));
            vertices8[2] = new Vertex(new Vector3(2, 4, -0.04f));

            //side face 2 triangle 2 texturevertices
            TextureVertex[] textureVertices8 = new TextureVertex[3];
            textureVertices8[0] = new TextureVertex(new Vector2(0, 0));
            textureVertices8[1] = new TextureVertex(new Vector2(1, 1));
            textureVertices8[2] = new TextureVertex(new Vector2(0, 1));

            //top face 1 triangle 1
            Vertex[] vertices9 = new Vertex[3];
            vertices9[0] = new Vertex(new Vector3(0, 4, -0.04f));
            vertices9[1] = new Vertex(new Vector3(2, 4, -0.04f));
            vertices9[2] = new Vertex(new Vector3(2, 4, 0.04f));

            //top face 1 triangle 1 texturevertices
            TextureVertex[] textureVertices9 = new TextureVertex[3];
            textureVertices9[0] = new TextureVertex(new Vector2(0, 0));
            textureVertices9[1] = new TextureVertex(new Vector2(1, 0));
            textureVertices9[2] = new TextureVertex(new Vector2(1, 1));

            //top face 1 triangle 2
            Vertex[] vertices10 = new Vertex[3];
            vertices10[0] = new Vertex(new Vector3(0, 4, -0.04f));
            vertices10[1] = new Vertex(new Vector3(2, 4, 0.04f));
            vertices10[2] = new Vertex(new Vector3(0, 4, 0.04f));

            //top face 1 triangle 2 texturevertices
            TextureVertex[] textureVertices10 = new TextureVertex[3];
            textureVertices10[0] = new TextureVertex(new Vector2(0, 0));
            textureVertices10[1] = new TextureVertex(new Vector2(1, 1));
            textureVertices10[2] = new TextureVertex(new Vector2(0, 1));

            //bottom face 1 triangle 1
            Vertex[] vertices11 = new Vertex[3];
            vertices11[0] = new Vertex(new Vector3(0, 0, 0.04f));
            vertices11[1] = new Vertex(new Vector3(2, 0, 0.04f));
            vertices11[2] = new Vertex(new Vector3(2, 0, -0.04f));

            //bottom face 1 triangle 1 texturevertices
            TextureVertex[] textureVertices11 = new TextureVertex[3];
            textureVertices11[0] = new TextureVertex(new Vector2(0, 0));
            textureVertices11[1] = new TextureVertex(new Vector2(1, 0));
            textureVertices11[2] = new TextureVertex(new Vector2(1, 1));

            //bottom face 1 triangle 2
            Vertex[] vertices12 = new Vertex[3];
            vertices12[0] = new Vertex(new Vector3(0, 0, 0.04f));
            vertices12[1] = new Vertex(new Vector3(2, 0, -0.04f));
            vertices12[2] = new Vertex(new Vector3(0, 0, -0.04f));

            //bottom face 1 triangle 2 texturevertices
            TextureVertex[] textureVertices12 = new TextureVertex[3];
            textureVertices12[0] = new TextureVertex(new Vector2(0, 0));
            textureVertices12[1] = new TextureVertex(new Vector2(1, 1));
            textureVertices12[2] = new TextureVertex(new Vector2(0, 1));

            Face[] faces = new Face[12];
            faces[0] = new Face(vertices1, textureVertices1);
            faces[1] = new Face(vertices2, textureVertices2);
            faces[2] = new Face(vertices3, textureVertices3);
            faces[3] = new Face(vertices4, textureVertices4);
            faces[4] = new Face(vertices5, textureVertices5);
            faces[5] = new Face(vertices6, textureVertices6);
            faces[6] = new Face(vertices7, textureVertices7);
            faces[7] = new Face(vertices8, textureVertices8);
            faces[8] = new Face(vertices9, textureVertices9);
            faces[9] = new Face(vertices10, textureVertices10);
            faces[10] = new Face(vertices11, textureVertices11);
            faces[11] = new Face(vertices12, textureVertices12);

            Transform prefab = new Transform("wall");
            prefab.Faces = faces;
            prefab.Width = 2;
            prefab.Height = 4;
            return prefab;
        }
    }
}
