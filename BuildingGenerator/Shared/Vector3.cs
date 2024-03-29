﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.Shared
{
    public class Vector3
    {
        private float m_X, m_Y, m_Z;
        public float x { get { return m_X; } set { m_X = value; } }
        public float y { get { return m_Y; } set { m_Y = value; } }
        public float z { get { return m_Z; } set { m_Z = value; } }

        public Vector3(float x, float y, float z)
        {
            m_X = x;
            m_Y = y;
            m_Z = z;
        }
        public Vector3(int x, int y, int z)
        {
            m_X = x;
            m_Y = y;
            m_Z = z;
        }
        public void Set(float x, float y, float z)
        {
            m_X = x;
            m_Y = y;
            m_Z = z;
        }
        
        public void Subtract(Vector3 vectorToSubtract)
        {
            m_X = m_X - vectorToSubtract.x;
            m_Y = m_Y - vectorToSubtract.y;
            m_Z = m_Z - vectorToSubtract.z;
            
        }
        public Vector3 SubtractWithReturn(Vector3 vectorToSubtract)
        {
            return new Vector3(m_X - vectorToSubtract.x, m_Y - vectorToSubtract.y, m_Z - vectorToSubtract.z);
        }
        
        public void Subtract(Vector3Int vectorToSubtract)
        {
            m_X = m_X - vectorToSubtract.x;
            m_Y = m_Y - vectorToSubtract.y;
            m_Z = m_Z - vectorToSubtract.z;

        }
        public void Add(Vector3 vectorToAdd)
        {
            m_X = m_X + vectorToAdd.x;
            m_Y = m_Y + vectorToAdd.y;
            m_Z = m_Z + vectorToAdd.z;
        }
        public void Add(Vector3Int vectorToAdd)
        {
            m_X = m_X + vectorToAdd.x;
            m_Y = m_Y + vectorToAdd.y;
            m_Z = m_Z + vectorToAdd.z;
        }

        public void Multiply(Vector3 multiplier)
        {
            m_X = m_X * multiplier.x;
            m_Y = m_Y * multiplier.y;
            m_Z = m_Z * multiplier.z;
        }

        public void Multiply(Vector3Int multiplier)
        {
            m_X = m_X * multiplier.x;
            m_Y = m_Y * multiplier.y;
            m_Z = m_Z * multiplier.z;
        }

        public string ToString()
        {
            // Cultureinfo stuff needed because of the decimal dot is needed
            // .obj files do not work with decimal commas so this is necessary
            return $"v {m_X.ToString(CultureInfo.GetCultureInfo("en-US"))} " +
                $"{m_Y.ToString(CultureInfo.GetCultureInfo("en-US"))} " +
                $"{m_Z.ToString(CultureInfo.GetCultureInfo("en-US"))}";// +
                //$"\n";
        }

        public static bool operator ==(Vector3 a, Vector3 b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }
        public static bool operator !=(Vector3 a, Vector3 b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Vector3 p = obj as Vector3;
            if ((System.Object)p == null)
                return false;

            return (x == p.x) && (y == p.y) && (z == p.z);
        }
    }
}
