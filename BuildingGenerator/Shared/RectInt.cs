using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.Shared
{
    public class RectInt
    {
        private int m_XMin, m_YMin, m_Width, m_Height;
        public int x { get { return m_XMin; } set { m_XMin = value; } }
        public int y { get { return m_YMin; } set { m_YMin = value; } }
        public int width { get { return m_Width; } set { m_Width = value; } }
        public int height { get { return m_Height; } set { m_Height = value; } }
        public int xMin { get { return Math.Min(m_XMin, m_XMin + m_Width); } set { int oldxmax = xMax; m_XMin = value; m_Width = oldxmax - m_XMin; } }
        public int yMin { get { return Math.Min(m_YMin, m_YMin + m_Height); } set { int oldymax = yMax; m_YMin = value; m_Height = oldymax - m_YMin; } }
        public int xMax { get { return Math.Max(m_XMin, m_XMin + m_Width); } set { m_Width = value - m_XMin; } }
        public int yMax { get { return Math.Max(m_YMin, m_YMin + m_Height); } set { m_Height = value - m_YMin; } }
        public Vector2Int position { get { return new Vector2Int(m_XMin, m_YMin); } set { m_XMin = value.x; m_YMin = value.y; } }
        public Vector2Int size { get { return new Vector2Int(m_Width, m_Height); } set { m_Width = value.x; m_Height = value.y; } }
        public Vector2Int min { get { return new Vector2Int(xMin, yMin); } set { xMin = value.x; yMin = value.y; } }
        public Vector2Int max { get { return new Vector2Int(xMax, yMax); } set { xMax = value.x; yMax = value.y; } }
        public Vector2 center { get { return new Vector2(x + m_Width / 2f, y + m_Height / 2f); } }

        public RectInt(int xMin, int yMin, int width, int height)
        {
            m_XMin = xMin;
            m_YMin = yMin;
            m_Width = width;
            m_Height = height;
        }

        public RectInt(Vector2Int position, Vector2Int size)
        {
            m_XMin = position.x;
            m_YMin = position.y;
            m_Width = size.x;
            m_Height = size.y;
        }

        public bool Contains(Vector2Int position)
        {
            return position.x >= xMin
                && position.y >= yMin
                && position.x < xMax
                && position.y < yMax;
        }

        public bool Contains(int x, int y)
        {
            return x >= xMin
                && y >= yMin
                && x < xMax
                && y < yMax;
        }

        public bool IsInside(int x, int y)
        {
            return x > xMin
                && y > yMin
                && x < xMax
                && y < yMax;
        }

        public bool Overlaps(RectInt other)
        {
            return other.xMin < xMax
                && other.xMax > xMin
                && other.yMin < yMax
                && other.yMax > yMin;
        }
        public bool Equals(RectInt other)
        {
            return m_XMin == other.m_XMin &&
                m_YMin == other.m_YMin &&
                m_Width == other.m_Width &&
                m_Height == other.m_Height;
        }

        public Vector3Int[] ToVector3Int()
        {
            return new Vector3Int[]
            {
                new Vector3Int(xMin, yMin, 0),
                new Vector3Int(xMax, yMin, 0),
                new Vector3Int(xMax, yMax, 0),
                new Vector3Int(xMin, yMax, 0)
            };
        }

        public Vector3Int[] ToVector3Int(int height)
        {
            return new Vector3Int[]
            {
                new Vector3Int(xMin, yMin, height),
                new Vector3Int(xMax, yMin, height),
                new Vector3Int(xMax, yMax, height),
                new Vector3Int(xMin, yMax, height)
            };
        }

        public Vector2Int[] ToVector2Int()
        {
            return new Vector2Int[]
            {
                new Vector2Int(xMin, yMin),
                new Vector2Int(xMax, yMin),
                new Vector2Int(xMax, yMax),
                new Vector2Int(xMin, yMax)
            };
        }

        public RectInt Intersect(RectInt other)
        {
            if (!Overlaps(other))
            {
                return new RectInt(0, 0, 0, 0);
            }

            return new RectInt(
                Math.Max(xMin, other.xMin),
                Math.Max(yMin, other.yMin),
                Math.Min(xMax, other.xMax) - Math.Max(xMin, other.xMin),
                Math.Min(yMax, other.yMax) - Math.Max(yMin, other.yMin)
            );
        }

    }
}
