using BuildingGenerator.Serialization;
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

        public float Width { get; set; }
        public float Height { get; set; }

        public Face[] Faces { get; set; }

        public Transform()
        {
            Name = "Transform";
            Position = new Vector3(0, 0, 0);
            Width = 0;
            Height = 0;
            Faces = new Face[0];
        }

        public Transform(string name)
        {
            Name = name;
            Position = new Vector3(0, 0, 0);
            Width = 0;
            Height = 0;
            Faces = new Face[0];
        }

        public Transform(Transform prefab, Vector3Int position, Quaternion rotation)
        {
            Name = prefab.Name;
            Position = position.AsVec3();
            Width = prefab.Width;
            Height = prefab.Height;
            Faces = prefab.Faces.Length > 0 ? new Face[prefab.Faces.Length] : new Face[0];
            for (int i = 0; i < prefab.Faces.Length; i++)
            {
                Faces[i] = prefab.Faces[i].Clone(true);
                Faces[i].Rotate(rotation);
                Faces[i].Translate(position);
            }

            _SetVertexIds();
        }

        public Transform(Transform prefab, Vector3 position, Quaternion rotation)
        {
            Name = prefab.Name;
            Position = position;
            Width = prefab.Width;
            Height = prefab.Height;
            Faces = prefab.Faces.Length > 0 ? new Face[prefab.Faces.Length] : new Face[0];
            for (int i = 0; i < prefab.Faces.Length; i++)
            {
                Faces[i] = prefab.Faces[i].Clone(false);
                Faces[i].Rotate(rotation);
                Faces[i].Translate(position);
            }

            _SetVertexIds();
        }
        /*
        public Transform(Obj obj)
        { 
            Faces = 
        }
        */
        public void Translate(Vector3 vector)
        {
            for (int i = 0; i < Faces.Length; i++)
            {
                Faces[i].Translate(vector);
            }
            Position.Add(vector);
        }

        public void Rotate(Quaternion q)
        {
            
            for (int i = 0; i < Faces.Length; i++)
            {
                Faces[i].Rotate(q);
            }
        }

        private void _SetVertexIds()
        {
            foreach (var face in Faces)
            {
                face.SetVertexIds();
            }
        }

        public void SetParent(Transform parent)
        {
            _parent = parent;
        }

        public Vector3 TransformPoint(Vector3 vector)
        {
            return new Vector3(Position.x + vector.x, Position.y + vector.y, Position.z + vector.z);
        }

        public string VerticesToString()
        {
            string result = "";
            foreach (var face in Faces)
            {
                // has to be in reverse, because in unity the vertices have to be in clockwise order
                // while we generate them in counter clockwise order
                // also Unity flips them regardless (more info in IPrefab.cs), so this is necessary
                foreach (var vertex in face.Vertices.Reverse())
                {
                    result += vertex.ToString();
                }
            }
            return result;
        }

        public string FacesToString()
        {
            string result = "";
            foreach (var face in Faces)
            {
                result += face.ToString();
            }
            return result;
        }
    }
}
