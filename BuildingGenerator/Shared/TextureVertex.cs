using BuildingGenerator.Serialization;
using BuildingGenerator.Serialization.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.Shared
{
    public class TextureVertex : IFromObjFile
    {
        private long _id = 0;
        private Vector2 _position = new Vector2(0, 0);
        private const int _minimumDataLength = 3;
        private const string _prefix = "vt";
        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public void LoadFromStringArray(string[] data)
        {
            if (data.Length < _minimumDataLength)
                throw new ArgumentException("Input array must be of minimum length " + _minimumDataLength, "data");

            if (!data[0].ToLower().Equals(_prefix))
                throw new ArgumentException("Data prefix must be '" + _prefix + "'", "data");

            bool success;

            double x, y;

            success = double.TryParse(data[1], NumberStyles.Any, CultureInfo.InvariantCulture, out x);
            if (!success) throw new ArgumentException("Could not parse X parameter as double");

            success = double.TryParse(data[2], NumberStyles.Any, CultureInfo.InvariantCulture, out y);
            if (!success) throw new ArgumentException("Could not parse Y parameter as double");

            _position = new Vector2((float)x, (float)y);
            _id = VertexIdProvider.GetNextId();
        }

        public override string ToString()
        {
            return string.Format("vt {0} {1}", Position.x, Position.y);
        }
    }
}
