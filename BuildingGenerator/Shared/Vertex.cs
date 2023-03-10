using BuildingGenerator.Serialization;
using BuildingGenerator.Serialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.Shared
{
    public class Vertex : IFromObjFile
    {
        private long _id = 0;
        private Vector3 _position = new Vector3(0, 0, 0);
        private const int _minimumDataLength = 4;
        private const string _prefix = "v";

        public long Id { 
            get { return _id; }
            set { _id = value; }
        }
        public Vector3 Position { 
            get { return _position; } 
            set { _position = value; } 
        }

        public Vertex()
        {
            _id= 0;
            _position = new Vector3(0, 0, 0);
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

        public Vertex(long id)
        {
            _id = id;
            _position = new Vector3(0, 0, 0);
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
        /*
        public override string ToString()
        {
            return string.Format("v {0} {1} {2}\n", _position.x, _position.y, _position.z);
        }
        */

        public void LoadFromStringArray(string[] data)
        {
            if (data.Length < _minimumDataLength)
                throw new ArgumentException("Input array must be of minimum length " + _minimumDataLength, "data");

            if (!data[0].ToLower().Equals(_prefix))
                throw new ArgumentException("Data prefix must be '" + _prefix + "'", "data");

            bool success;

            double x, y, z;

            success = double.TryParse(data[1], NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out x);
            if (!success) throw new ArgumentException("Could not parse X parameter as double");

            success = double.TryParse(data[2], NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out y);
            if (!success) throw new ArgumentException("Could not parse Y parameter as double");

            success = double.TryParse(data[3], NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out z);
            if (!success) throw new ArgumentException("Could not parse Z parameter as double");

            _position = new Vector3((float)x, (float)y, (float)z);
            _id = VertexIdProvider.GetNextId();
 
        }

        public override string ToString()
        {
            return _position.ToString();
        }
        
    }
}
