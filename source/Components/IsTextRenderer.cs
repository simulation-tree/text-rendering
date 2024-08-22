using Simulation;

namespace Rendering.Components
{
    public struct IsTextRenderer
    {
        public rint meshReference;
        public rint materialReference;
        public rint cameraReference;
        public rint fontReference;

        public IsTextRenderer(rint meshReference, rint materialReference, rint cameraReference, rint fontReference)
        {
            this.meshReference = meshReference;
            this.materialReference = materialReference;
            this.cameraReference = cameraReference;
            this.fontReference = fontReference;
        }
    }
}