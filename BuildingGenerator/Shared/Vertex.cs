using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.Shared
{
    public class Vertex
    {
        public long Id { get; set; }
        public Vector3 Position { get; set; }

        //this function should clone a vertex
        public Vertex Clone()
        {
            return new Vertex
            {
                Id = Id,
                Position = new Vector3(Position.x, Position.y, Position.z)
            };
        }

        public Vertex Add(Vector3 vectorToAdd)
        {
            return new Vertex
            {
                Id = Id,
                Position = Position.Add(vectorToAdd)
            };
        }

        public Vertex Subtract(Vector3 vectorToSubtract)
        {
            return new Vertex
            {
                Id = Id,
                Position = Position.Subtract(vectorToSubtract)
            };
        }

        public Vertex Add(Vector3Int vectorToAdd)
        {
            return new Vertex
            {
                Id = Id,
                Position = Position.Add(vectorToAdd)
            };
        }

        public Vertex Subtract(Vector3Int vectorToSubtract)
        {
            return new Vertex
            {
                Id = Id,
                Position = Position.Subtract(vectorToSubtract)
            };
        }

        public Vertex Add(Vertex vertexToAdd)
        {
            return new Vertex
            {
                Id = Id,
                Position = Position.Add(vertexToAdd.Position)
            };
        }

        public Vertex Subtract(Vertex vertexToSubtract)
        {
            return new Vertex
            {
                Id = Id,
                Position = Position.Subtract(vertexToSubtract.Position)
            };
        }

        public string ToString()
        {
            return Position.ToString();
        }
    }
}
