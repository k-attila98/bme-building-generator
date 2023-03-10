using BuildingGenerator.Serialization;
using BuildingGenerator.Serialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.Shared
{
    public class Face : IFromObjFile
    {
        private Vertex[] _vertices = new Vertex[0];
        private TextureVertex[] _textureVertices = new TextureVertex[0];
        private Vector3 _normal = new Vector3(0, 0, 0);

        private const int _minimumDataLength = 4;
        private const string _prefix = "f";
        public string UseMtl { get; set; }

        public Face() { }

        public Face(Vertex[] vertices)
        {
            if (vertices.Length != 3)
            {
                throw new ArgumentException("Face must have 3 vertices");
            }
            _vertices = vertices;
            _normal = _CalculateNormal(_vertices[0], _vertices[1], _vertices[2]);
        }
        public Vertex[] Vertices {
            get { return _vertices; }
            set
            {
                if (value.Length != 3)
                {
                    throw new ArgumentException("Face must have 3 vertices");
                }

                _vertices = value;
                _normal = _CalculateNormal(_vertices[0], _vertices[1], _vertices[2]);
            }
        }

        public TextureVertex[] TextureVertices
        {
            get { return _textureVertices; }
            set
            {
                /*
                if (value.Length != 3)
                {
                    throw new ArgumentException("Face must have 3 textureVertices");
                }
                */

                _textureVertices = value;
                //_normal = _CalculateNormal(_vertices[0], _vertices[1], _vertices[2]);
            }
        }

        // At the moment this is not used anywhere, only calculated
        // Might be useful in the future
        public Vector3 Normal { 
            get { return _normal; } 
            set { _normal = value; } 
        }

        public void AddVertex(Vertex vertex)
        {
            if (_vertices.Length < 3)
            {
                List<Vertex> vertices = _vertices.ToList();
                vertices.Add(vertex);
                _vertices = vertices.ToArray();
            }
        }

        public void SetVertexIds()
        {
            foreach (var vertex in _vertices)
            {
                vertex.Id = VertexIdProvider.GetNextId();
            }
        }

        //this function should clone the face
        public Face Clone(bool isDeepClone)
        {
            List<Vertex> vertices = new List<Vertex>();
            foreach (var vertex in _vertices)
            {
                vertices.Add(vertex.Clone(isDeepClone));
            }
            Face face = new Face(vertices.ToArray());
            return face;
        }

        private Vector3Int _CalculateNormal(Vector3Int vertex1, Vector3Int vertex2, Vector3Int vertex3)
        {
            var v1 = vertex2.Subtract(vertex1);
            var v2 = vertex3.Subtract(vertex1);
            Vector3Int normal = new Vector3Int(
                    (v1.y * v2.z) - (v1.z - v2.y),
                    -((v2.z * v1.x) - (v2.x * v1.z)),
                    (v1.x * v2.y) - (v1.y * v2.x)
                );

            return normal;
        }

        private Vector3 _CalculateNormal(Vertex vertex1, Vertex vertex2, Vertex vertex3)
        {
            var v1 = vertex2.SubtractWithReturn(vertex1);
            var v2 = vertex3.SubtractWithReturn(vertex1);
            Vector3 normal = new Vector3(
                    (v1.Position.y * v2.Position.z) - (v1.Position.z * v2.Position.y),
                    (v1.Position.z * v2.Position.x) - (v1.Position.x * v2.Position.z),
                    (v1.Position.x * v2.Position.y) - (v1.Position.y * v2.Position.x)
                );

            return normal;
        }


        public void Translate(Vector3Int vector)
        {
            for (int i = 0; i < _vertices.Length; i++)
            {
                _vertices[i].Add(vector);
            }
            _normal = _CalculateNormal(_vertices[0], _vertices[1], _vertices[2]);
        }

        public void Translate(Vector3 vector)
        {
            for (int i = 0; i < _vertices.Length; i++)
            {
                _vertices[i].Add(vector);
            }
            _normal = _CalculateNormal(_vertices[0], _vertices[1], _vertices[2]);
        }

        public void Rotate(Quaternion q)
        {
            for (int i = 0; i < _vertices.Length; i++)
            {
                _vertices[i].Position = _Multiply(q, _vertices[i].Position);
            }
            _normal = _CalculateNormal(_vertices[0], _vertices[1], _vertices[2]);
        }

        private Vector3 _Multiply(Quaternion q, Vector3 v)
        {
            var qv = new Quaternion(v.x, v.y, v.z, 0);
            var qv2 = q * qv * Quaternion.Conjugate(q);
            return new Vector3(
                // the rounding is needed because the conversion between radians and degrees is not exact
                // thus the rotation is not perfect with simple multiplication
                // this snaps the vertex to the nearest point (useful near integers for example and unit tests)
                MathHelper.RoundToNearestFloatWithDecimals(qv2.X, 3),
                MathHelper.RoundToNearestFloatWithDecimals(qv2.Y, 3),
                MathHelper.RoundToNearestFloatWithDecimals(qv2.Z, 3)
            );
        }

        // TODO: emiatt a függvény miatt átgondolnik, hogy lehet inkább csak az indexeket kéne tárolni a facenek és nem a vertexeket
        // viszont ez kihathat a többi részre is, mert a facekből gyűjtjük ki a vertexeket, szóval lehet érdemes egy központi tárolóba kiszervezni a vertexeket
        // és onnan egy resolve függvénnyel kihalászni a dolgokat
        public void LoadFromStringArray(string[] data)
        {
            if (data.Length < _minimumDataLength)
                throw new ArgumentException("Input array must be of minimum length " + _minimumDataLength, "data");

            if (!data[0].ToLower().Equals(_prefix))
                throw new ArgumentException("Data prefix must be '" + _prefix + "'", "data");

            int vcount = data.Count() - 1;
            _vertices = new Vertex[vcount];
            _textureVertices = new TextureVertex[vcount];

            bool success;

            for (int i = 0; i < vcount; i++)
            {
                string[] parts = data[i + 1].Split('/');

                int vindex;
                success = int.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out vindex);
                if (!success) throw new ArgumentException("Could not parse parameter as int");
                _vertices[i] = new Vertex(vindex);

                if (parts.Count() > 1)
                {
                    success = int.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out vindex);
                    if (success)
                    {
                        _textureVertices[i] = new TextureVertex(vindex);
                    }
                }
            }
        }

        // HACKHACK this will write invalid files if there are no texture vertices in
        // the faces, need to identify that and write an alternate format
        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            b.Append("f");

            for (int i = 0; i < _vertices.Count(); i++)
            {
                if (i < _textureVertices.Length)
                {
                    b.AppendFormat(" {0}/{1}", _vertices[i].Id, _textureVertices[i].Id);
                }
                else
                {
                    b.AppendFormat(" {0}", _vertices[i].Id);
                }
            }
            //b.Append("\n");
            return b.ToString();
        }
        
        /*
        public string ToString()
        {
            return $"f {_vertices[0].Id} {_vertices[1].Id} {_vertices[2].Id}\n";
        }
        */

        
    }
}
