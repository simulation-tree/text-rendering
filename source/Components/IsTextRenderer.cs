using Worlds;

namespace Rendering.Components
{
    public struct IsTextRenderer
    {
        public rint textMeshReference;
        public rint materialReference;
        public rint fontReference;
        public LayerMask renderMask;

        public IsTextRenderer(rint meshReference, rint materialReference, rint fontReference, LayerMask renderMask)
        {
            this.textMeshReference = meshReference;
            this.materialReference = materialReference;
            this.fontReference = fontReference;
            this.renderMask = renderMask;
        }
    }
}