using UnityEngine;
using UnityEngine.AI;

public class NavMeshSurfaceGenerator
{
    public void GenerateNavMeshSurface(NavMeshSurface surface) => surface.BuildNavMesh();
}
