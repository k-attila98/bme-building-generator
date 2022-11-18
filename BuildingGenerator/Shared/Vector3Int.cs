using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.Shared
{
    public class Vector3Int
    {
        private int m_X, m_Y, m_Z;
        public int x { get { return m_X; } set { m_X = value; } }
        public int y { get { return m_Y; } set { m_Y = value; } }
        public int z { get { return m_Z; } set { m_Z = value; } }

        public Vector3Int(int x, int y, int z)
        {
            m_X = x;
            m_Y = y;
            m_Z = z;
        }
        public void Set(int x, int y, int z)
        {
            m_X = x;
            m_Y = y;
            m_Z = z;
        }

        public Vector3Int Subtract(Vector3Int vectorToSubtract)
        {
            return new Vector3Int(m_X - vectorToSubtract.x, m_Y - vectorToSubtract.y, m_Z - vectorToSubtract.z);

        }

        public Vector3Int Add(Vector3Int vectorToAdd)
        {
            return new Vector3Int(m_X + vectorToAdd.x, m_Y + vectorToAdd.y, m_Z + vectorToAdd.z);
        }

        public Vector3 AsVec3()
        {
            return new Vector3(m_X, m_Y, m_Z);
        }
    }
}
