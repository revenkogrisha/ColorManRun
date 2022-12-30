using ColorManRun.Level;
using UnityEngine;

namespace ColorManRun.Generators
{
    public interface ISurfaceGenerator
    {
        public FinishTarget GenerateSurface(Color[] colors);
    }
}
