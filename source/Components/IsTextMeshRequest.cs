using Worlds;

namespace Rendering.Components
{
    [Component]
    public struct IsTextMeshRequest
    {
        public bool loaded;
        public rint fontReference;

        public IsTextMeshRequest(rint fontReference)
        {
            this.loaded = false;
            this.fontReference = fontReference;
        }
    }
}