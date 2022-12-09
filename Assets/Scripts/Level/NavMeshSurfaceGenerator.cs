using UnityEngine.AI;

namespace SaveTheGuy.Level
{
    public class NavMeshSurfaceGenerator : INavMeshSurfaceGenerator
    {
        public void GenerateSurface(NavMeshSurface surface) => surface.BuildNavMesh();
    }
}
