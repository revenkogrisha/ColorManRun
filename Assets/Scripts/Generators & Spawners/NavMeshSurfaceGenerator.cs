using UnityEngine.AI;

namespace ColorManRun.Generators
{
    public class NavMeshSurfaceGenerator : INavMeshSurfaceGenerator
    {
        public void GenerateSurface(NavMeshSurface surface) => surface.BuildNavMesh();
    }
}
