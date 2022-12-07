using UnityEngine.AI;

public class NavMeshSurfaceGenerator : INavMeshSurfaceGenerator
{
    public void GenerateSurface(NavMeshSurface surface) => surface.BuildNavMesh();
}
