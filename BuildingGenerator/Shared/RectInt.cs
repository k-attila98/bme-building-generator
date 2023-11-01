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

        public bool ContainsWithEdges(int x, int y)
        {
            return x >= xMin
                && y >= yMin
                && x <= xMax
                && y <= yMax;
        }

        public bool IsInside(int x, int y)
        {
            return x > xMin
                && y > yMin
                && x < xMax
                && y < yMax;
        }

        // this function should be true if another rectint has all parts inside this rectint
        public bool Contains(RectInt other)
        {
            return other.xMin >= xMin
                && other.yMin >= yMin
                && other.xMax <= xMax
                && other.yMax <= yMax;
        }

        public bool Overlaps(RectInt other)
        {
            return other.xMin < xMax
                && other.xMax > xMin
                && other.yMin < yMax
                && other.yMax > yMin;
        }
        
        public RectInt[] SubtractAndDivide(RectInt other)
        {
            RectInt[] result = new RectInt[0];

            result = SubtractEnclosed(other);
            if (result.Length == 4)
            {
                return result;
            }
            result = SubtractOnEdge(other);
            if(result.Length == 3)
            {
                return result;
            }
            result = SubtractOnCorner(other);
            
            if(result.Length == 2)
            {
                return result;
            }
            result = SubtractOnHalf(other);
            
            return result;
        }

        public RectInt[] SubtractOnHalf(RectInt other)
        {
            List<RectInt> rects = new List<RectInt>();
            // other on top of right half
            if (other.xMin > xMin && other.yMin < yMin)
                rects.Add(new RectInt(xMin, yMin, other.xMin - xMin, height));
            // other on top of left half
            else if (other.xMin <= xMin && other.yMin <= yMin && other.yMax >= yMax)
                rects.Add(new RectInt(other.xMax, yMin, xMax - other.xMax, height));
            // other on top of bottom half
            else if (other.xMin <= xMin && other.yMin <= yMin && other.xMax >= xMax)
                rects.Add(new RectInt(xMin, other.yMax, width, yMax - other.yMax));
            // other on top of top half
            else if (other.xMin <= xMin && other.yMin > yMin && other.yMax > yMax)
                rects.Add(new RectInt(xMin, yMin, width, other.yMin - yMin));
            

            return rects.ToArray();
        }

        public RectInt[] SubtractOnCorner(RectInt other)
        {
            List<RectInt> rects = new List<RectInt>();
            // other intersects with bottom right corner
            if (other.xMin > xMin && other.yMin < yMin)
            {
                rects.Add(new RectInt(xMin, yMin, other.xMin - xMin, height));
                rects.Add(new RectInt(other.xMin, other.yMax, xMax - other.xMin, yMax - other.yMax));
            }
            // other intersects with top right corner
            else if (other.xMin > xMin && other.yMin > yMin)
            {
                rects.Add(new RectInt(xMin, yMin, other.xMin - xMin, height));
                //if(other.yMin > yMin)
                rects.Add(new RectInt(other.xMin, yMin, xMax - other.xMin, other.yMin - yMin));
            }
            // other intersects with top left corner
            else if (other.xMin < xMin && other.yMin > yMin)
            {
                rects.Add(new RectInt(other.xMax, yMin, xMax - other.xMax, height));
                rects.Add(new RectInt(xMin, yMin, other.xMax - xMin, other.yMin - yMin));
            }
            // other intersects with bottom left corner
            else if (other.xMin < xMin && other.yMin < yMin)
            {
                rects.Add(new RectInt(other.xMax, yMin, xMax - other.xMax, height));
                rects.Add(new RectInt(xMin, other.yMax, other.xMax - xMin, yMax - other.yMax));
            }

            // other ends on bottom right corner
            else if (other.xMin > xMin && other.yMin <= yMin && other.xMax >= xMax && other.yMax < yMax)
            {
                rects.Add(new RectInt(xMin, yMin, other.xMin - xMin, height));
                rects.Add(new RectInt(other.xMin, other.yMax, xMax - other.xMin, yMax - other.yMax));
            }
            // other ends on top right corner
            else if (other.xMin > xMin && other.yMin > yMin && other.xMax >= xMax && other.yMax >= yMax)
            {
                rects.Add(new RectInt(xMin, yMin, other.xMin - xMin, height));
                rects.Add(new RectInt(other.xMin, yMin, xMax - other.xMin, other.yMin - yMin));
            }
            // other ends on top left corner
            else if (other.xMin <= xMin && other.yMin > yMin && other.xMax < xMax && other.yMax >= yMax)
            {
                rects.Add(new RectInt(other.xMax, yMin, xMax - other.xMax, height));
                rects.Add(new RectInt(xMin, yMin, other.xMax - xMin, other.yMin - yMin));
            }
            // other ends on bottom left corner
            else if (other.xMin <= xMin && other.yMin <= yMin && other.xMax < xMax && other.yMax < yMax)
            {
                rects.Add(new RectInt(other.xMax, yMin, xMax - other.xMax, height));
                rects.Add(new RectInt(xMin, other.yMax, other.xMax - xMin, yMax - other.yMax));
            }
            return rects.ToArray();
        }

        public RectInt[] SubtractOnEdge(RectInt other)
        {
            List<RectInt> rects = new List<RectInt>();
            //other only intersect with the bottom edge
            if (other.xMin > xMin && other.xMax < xMax && other.yMax < yMax)
            {
                // left, upright
                rects.Add(new RectInt(xMin, yMin, other.xMin - xMin, height));
                // middle, above the other
                rects.Add(new RectInt(other.xMin, other.yMax, other.width, yMax - other.yMax));
                // right, upright
                rects.Add(new RectInt(other.xMax, yMin, xMax - other.xMax, height));

            }
            // right edge
            else if (other.xMin > xMin && other.yMax < yMax && other.yMin > yMin)
            {
                // bottom, sideways
                rects.Add(new RectInt(xMin, yMin, width, other.yMin - yMin));
                // middle, sideways
                rects.Add(new RectInt(xMin, other.yMin, other.xMin - xMin, other.height));
                // top, sideways
                rects.Add(new RectInt(xMin, other.yMax, width, yMax - other.yMax));
            }
            // top edge
            else if (other.xMin > xMin && other.xMax < xMax && other.yMin > yMin)
            {
                // left, upright
                rects.Add(new RectInt(xMin, yMin, other.xMin - xMin, height));
                // bottom, sideways
                rects.Add(new RectInt(other.xMin, yMin, other.width, other.yMin - yMin));
                // right, sideways
                rects.Add(new RectInt(other.xMax, yMin, xMax - other.xMax, height));
            }
            // left edge
            else if (other.xMax < xMax && other.yMax < yMax && other.yMin > yMin)
            {
                // bottom, sideways
                rects.Add(new RectInt(xMin, yMin, width, other.yMin - yMin));
                // middle, sideways
                rects.Add(new RectInt(other.xMax, other.yMin, xMax - other.xMax, other.height));
                // top, sideways
                rects.Add(new RectInt(xMin, other.yMax, width, yMax - other.yMax));
            }
            return rects.ToArray();
        }

        public RectInt[] SubtractEnclosed(RectInt other)
        {
            List<RectInt> rects = new List<RectInt>();
            if(other.xMin > xMin && other.xMax < xMax && other.yMin > yMin && other.yMax < yMax)
            {
                // bottom sideways
                rects.Add(new RectInt(xMin, yMin, width, other.yMin - yMin));
                // right, upright
                rects.Add(new RectInt(other.xMax, other.yMin, xMax - other.xMax, other.height));
                // top, sideways
                rects.Add(new RectInt(xMin, other.yMax, width, yMax - other.yMax));
                // middle, sideways
                rects.Add(new RectInt(xMin, other.yMin, other.xMin - xMin, other.height));
            };

            return rects.ToArray();
        }

        public bool OverlapsWithEdges(RectInt other)
        {
            return other.xMin <= xMax
                && other.xMax >= xMin
                && other.yMin <= yMax
                && other.yMax >= yMin;
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
            if (!Overlaps(other)) //|| xMin == other.xMin || yMin == other.yMin)
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
