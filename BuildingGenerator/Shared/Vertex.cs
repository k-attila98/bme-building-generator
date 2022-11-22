using BuildingGenerator.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.Shared
{
    public class Vertex
    {
        public long Id { get; }
        public Vector3 Position { get; set; }

        public Vertex(Vector3 position)
        {
            Id = VertexIdProvider.GetNextId();
            Position = position;
        }

        public Vertex Clone()
        {
            return new Vertex(new Vector3(Position.x, Position.y, Position.z));
        }

        public Vertex Add(Vector3 vectorToAdd)
        {
            return new Vertex(Position.Add(vectorToAdd));
        }

        public Vertex Subtract(Vector3 vectorToSubtract)
        {
            return new Vertex(Position.Subtract(vectorToSubtract));
        }

        public Vertex Add(Vector3Int vectorToAdd)
        {
            return new Vertex(Position.Add(vectorToAdd));
        }

        public Vertex Subtract(Vector3Int vectorToSubtract)
        {
            return new Vertex(Position.Subtract(vectorToSubtract));
        }
        
        public Vertex Add(Vertex vertexToAdd)
        {
            return new Vertex(Position = Position.Add(vertexToAdd.Position));
        }

        public Vertex Subtract(Vertex vertexToSubtract)
        {
            return new Vertex(Position.Subtract(vertexToSubtract.Position));
        }

        public string ToString()
        {
            return Position.ToString();
        }
    }
}
