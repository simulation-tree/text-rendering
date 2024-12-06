using Worlds;

namespace Rendering.Components
{
    [Component]
    public struct IsTextMesh
    {
        public uint version;

        public IsTextMesh(uint version)
        {
            this.version = version;
        }
    }
}