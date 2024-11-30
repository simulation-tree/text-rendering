using Worlds;

namespace Rendering.Components
{
    [Component]
    public struct IsTextMeshRequest
    {
        public uint version;
        public rint fontReference;

        public IsTextMeshRequest(rint fontReference)
        {
            version = default;
            this.fontReference = fontReference;
        }
    }
}