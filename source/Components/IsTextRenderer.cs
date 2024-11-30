using Worlds;

namespace Rendering.Components
{
    [Component]
    public struct IsTextRenderer
    {
        public rint textMeshReference;
        public rint materialReference;
        public rint fontReference;
        public uint mask;

        public IsTextRenderer(rint meshReference, rint materialReference, rint fontReference, uint mask)
        {
            this.textMeshReference = meshReference;
            this.materialReference = materialReference;
            this.fontReference = fontReference;
            this.mask = mask;
        }
    }
}