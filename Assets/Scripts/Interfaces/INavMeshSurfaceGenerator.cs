using UnityEngine.AI;

namespace SaveTheGuy.Level
{
    public interface INavMeshSurfaceGenerator
    {
        public void GenerateSurface(NavMeshSurface surface);
    }
}
