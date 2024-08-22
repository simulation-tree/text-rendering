using Simulation;
using System.Numerics;

namespace Rendering.Components
{
    public struct IsTextMeshRequest
    {
        public uint version;
        public rint fontReference;
        public Vector2 alignment;

        public IsTextMeshRequest(rint fontReference, Vector2 alignment)
        {
            version = default;
            this.fontReference = fontReference;
            this.alignment = alignment;
        }
    }
}