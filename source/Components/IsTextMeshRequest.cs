using Simulation;

namespace Rendering.Components
{
    public struct IsTextMeshRequest
    {
        public uint version;
        public rint textReference;
        public rint fontReference;

        public IsTextMeshRequest(rint textReference, rint fontReference)
        {
            version = default;
            this.textReference = textReference;
            this.fontReference = fontReference;
        }
    }
}