using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.Shared
{
    public class Vector2Int
    {
        private int m_X, m_Y;
        public int x { get { return m_X; } set { m_X = value; } }
        public int y { get { return m_Y; } set { m_Y = value; } }

        public Vector2Int(int x, int y)
        {
            m_X = x;
            m_Y = y;
        }
        public void Set(int x, int y)
        {
            m_X = x;
            m_Y = y;
        }
    }
}
