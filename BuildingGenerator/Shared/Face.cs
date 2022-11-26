using BuildingGenerator.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.Shared
{
    public class Face
    {
        private Vertex[] _vertices = new Vertex[3];
        private Vector3 _normal = new Vector3(0, 0, 0);

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
            //face.Normal = new Vector3(Normal.x, Normal.y, Normal.z);
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
            //_normal = _CalculateNormal(Vertices[0], Vertices[1], Vertices[2]);
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
                (float)Math.Round(qv2.X, 3),
                (float)Math.Round(qv2.Y, 3),
                (float)Math.Round(qv2.Z, 3)
            );
        }

        private Vector3 _RotatePoint(Quaternion rotation, Vector3 point)
        {

            float x = rotation.X * 2F;
            float y = rotation.Y * 2F;
            float z = rotation.Z * 2F;
            float xx = rotation.X * x;
            float yy = rotation.Y * y;
            float zz = rotation.Z * z;
            float xy = rotation.X * y;
            float xz = rotation.X * z;
            float yz = rotation.Y * z;
            float wx = rotation.W * x;
            float wy = rotation.W * y;
            float wz = rotation.W * z;

            Vector3 res = new Vector3(
                    ((1F - (yy + zz)) * point.x + (xy - wz) * point.y + (xz + wy) * point.z),
                    ((xy + wz) * point.x + (1F - (xx + zz)) * point.y + (yz - wx) * point.z),
                    ((xz - wy) * point.x + (yz + wx) * point.y + (1F - (xx + yy)) * point.z)
                );
            return res;
        }
        
        public string ToString()
        {
            return $"f {_vertices[0].Id} {_vertices[1].Id} {_vertices[2].Id}\n";
        }
    }
}
