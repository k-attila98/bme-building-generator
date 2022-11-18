using BuildingGenerator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.Serialization.Interfaces
{
    public interface ISerializableToObj
    {
        Vector2Int[] GetVertices();
    }
}
