using UnityEngine.AI;

namespace ColorManRun.Generators
{
    public interface INavMeshSurfaceGenerator
    {
        public void GenerateSurface(NavMeshSurface surface);
    }
}
