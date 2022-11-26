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
        private long _id = 0;
        private Vector3 _position = new Vector3(0, 0, 0);
        
        public long Id { 
            get { return _id; }
            set { _id = value; }
        }
        public Vector3 Position { 
            get { return _position; } 
            set { _position = value; } 
        }

        public Vertex(Vector3 position)
        {
            //_id = VertexIdProvider.GetNextId();
            _position = position;
        }

        public Vertex(long id, Vector3 position)
        {
            _id = id;
            _position = position;
        }
        
        public Vertex Clone(bool isDeepClone)
        {
            return isDeepClone ?
                new Vertex(_id, new Vector3(_position.x, _position.y, _position.z)) :
                new Vertex(new Vector3(_position.x, _position.y, _position.z));
        }
        
        public void Add(Vector3 vectorToAdd)
        {
            _position.Add(vectorToAdd);
        }
        
        public void Subtract(Vector3 vectorToSubtract)
        {
            _position.Subtract(vectorToSubtract);
        }

        public void Add(Vector3Int vectorToAdd)
        {
            _position.Add(vectorToAdd);
        }
        
        public void Subtract(Vector3Int vectorToSubtract)
        {
            _position.Subtract(vectorToSubtract);
        }
        
        public void Add(Vertex vertexToAdd)
        {
            _position.Add(vertexToAdd.Position);
        }

        public void Subtract(Vertex vertexToSubtract)
        {
            _position.Subtract(vertexToSubtract.Position);
        }
        
        public Vertex SubtractWithReturn(Vertex vertexToSubtract)
        {
            return new Vertex(_position.SubtractWithReturn(vertexToSubtract.Position));
        }

        public string ToString()
        {
            return _position.ToString();
        }
    }
}
