using BuildingGenerator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingGenerator.Prefabs
{
    /**
     * IMPORTANT: The vertices of the triangles in the faces must be in counter clockwise order.
     * Unity uses a left hand coordinate system and flips the normals if the vertices are not in the correct order 
     * (it assumes that any imported .obj file is in right handed system).
     * Thus the wall or prefab will be transparent on that face.
     */
    public interface IPrefab
    {
        Transform GetTransform();
    }
}
