using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.Serialization
{
    public static class VertexIdProvider
    {
        private static long _id = 1;

        public static long GetNextId()
        {
            return _id++;
        }
    }
}
