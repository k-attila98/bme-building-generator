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

        public Face(Vertex[] vertices)
        {
            if (vertices.Length != 3)
            {
                throw new ArgumentException("Face must have 3 vertices");
            }
            _vertices = vertices;
            Normal = _CalculateNormal(_vertices[0], _vertices[1], _vertices[2]);
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
                Normal = _CalculateNormal(_vertices[0], _vertices[1], _vertices[2]);
            }
        }
        public Vector3 Normal { get; set; }

        public void AddVertex(Vertex vertex)
        {
            if (Vertices.Length < 3)
            {
                List<Vertex> vertices = Vertices.ToList();
                vertices.Add(vertex);
                Vertices = vertices.ToArray();
            }
        }

        //this function should clone the face
        public Face Clone()
        {
            
            Vertex[] vertices = new Vertex[Vertices.Length];
            foreach (var vertex in Vertices)
            {
                vertices.Append(vertex.Clone());
            }
            Face face = new Face(vertices);
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
            var v1 = vertex2.Subtract(vertex1);
            var v2 = vertex3.Subtract(vertex1);
            Vector3 normal = new Vector3(
                    (v1.Position.y * v2.Position.z) - (v1.Position.z * v2.Position.y),
                    (v1.Position.z * v2.Position.x) - (v1.Position.x * v2.Position.z),
                    (v1.Position.x * v2.Position.y) - (v1.Position.y * v2.Position.x)
                );

            return normal;
        }


        public void Translate(Vector3Int vector)
        {
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i] = Vertices[i].Add(vector);
            }
            Normal = _CalculateNormal(Vertices[0], Vertices[1], Vertices[2]);
        }

        public void Translate(Vector3 vector)
        {
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i] = Vertices[i].Add(vector);
            }
            Normal = _CalculateNormal(Vertices[0], Vertices[1], Vertices[2]);
        }

        public void Rotate(Quaternion q)
        {
            Normal = _CalculateNormal(Vertices[0], Vertices[1], Vertices[2]);
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i].Position = _Multiply(q, Vertices[i].Position);
            }
            Normal = _CalculateNormal(Vertices[0], Vertices[1], Vertices[2]);
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
            return $"f {Vertices[0].Id} {Vertices[1].Id} {Vertices[2].Id}\n";
        }
    }
}
