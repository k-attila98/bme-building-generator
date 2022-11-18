using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.Shared
{
    public class Transform
    {
        private Transform _parent;
        public Transform Parent { 
            get { return _parent; } 
            set { SetParent(value); } 
        }
        public string Name { get; set; }
        public Vector3 Position { get; set; }


        // TODO: ezeket valahogy kalkulált mezőkké tenni, nem megadottnak
        public int Width { get; set; }
        public int Height { get; set; }

        Vector3[] Normals { get; set; }
        Vector3[] Vertices
        {
            get { return Vertices; }
            set
            {
                Vertices = value;
                Normals = new Vector3[0];
                for (int i = 0; i < Vertices.Length; i++)
                {
                    Normals.Append(_CalculateNormal(Vertices[i], Vertices[(i + 1) % Vertices.Length], Vertices[(i + 2) % Vertices.Length]));
                }
            }
        }
        

        public Transform()
        {
            Name = "Transform";
            Position = new Vector3(0, 0, 0);
            Width = 0;
            Height = 0;
            Vertices = new Vector3[0];
            Normals = new Vector3[0];
        }

        public Transform(string name)
        {
            Name = name;
            Position = new Vector3(0, 0, 0);
            Width = 0;
            Height = 0;
            Vertices = new Vector3[0];
            Normals = new Vector3[0];
        }

        public Transform(Transform prefab, Vector3Int position, Quaternion rotation)
        {
            Name = prefab.Name;
            Position = position.AsVec3();
            Width = prefab.Width;
            Height = prefab.Height;
            Vertices = new Vector3[prefab.Vertices.Length];
            for (int i = 0; i < prefab.Vertices.Length; i++)
            {
                Vertices[i] = prefab.Vertices[i];
            }

            this.Rotate(rotation);

            
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i] = Vertices[i].Add(position);
            }
        }

        public Transform(Transform prefab, Vector3 position, Quaternion rotation)
        {
            Name = prefab.Name;
            Position = position;
            Width = prefab.Width;
            Height = prefab.Height;
            Vertices = new Vector3[prefab.Vertices.Length];
            for (int i = 0; i < prefab.Vertices.Length; i++)
            {
                Vertices[i] = prefab.Vertices[i];
            }

            this.Rotate(rotation);


            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i] = Vertices[i].Add(position);
            }
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

        private Vector3 _CalculateNormal(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3)
        {
            var v1 = vertex2.Subtract(vertex1);
            var v2 = vertex3.Subtract(vertex1);
            Vector3 normal = new Vector3(
                    (v1.y * v2.z) - (v1.z - v2.y),
                    -((v2.z * v1.x) - (v2.x * v1.z)),
                    (v1.x * v2.y) - (v1.y * v2.x)
                );

            return normal;
        }

        // This function should translate the vertices by the given vector
        public void Translate(Vector3Int vector)
        {
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i] = Vertices[i].Add(vector);
            }
        }


        // This function should rotate the vertices around the origin with quaternion q
        public void Rotate(Quaternion q)
        {
            Normals = new Vector3[0];
            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i] = _Multiply(q, Vertices[i]);
                Normals.Append(_CalculateNormal(Vertices[i], Vertices[(i + 1) % Vertices.Length], Vertices[(i + 2) % Vertices.Length]));
            }
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

        // rotate vector with quaternion
        private Vector3 _Multiply(Quaternion q, Vector3 v)
        {
            var qv = new Quaternion(v.x, v.y, v.z, 0);
            var qv2 = q * qv * Quaternion.Conjugate(q);
            return new Vector3(qv2.X, qv2.Y, qv2.Z);
        }

        public void SetParent(Transform parent)
        {
            _parent = parent;
        }

        public Vector3 TransformPoint(Vector3 vector)
        {
            return new Vector3(Position.x + vector.x, Position.y + vector.y, Position.z + vector.z);
        }
    }
}
