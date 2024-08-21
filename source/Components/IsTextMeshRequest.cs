using Simulation;

namespace Rendering.Components
{
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