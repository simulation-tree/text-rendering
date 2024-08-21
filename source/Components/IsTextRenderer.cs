using Simulation;

namespace Rendering.Components
{
    public struct IsTextRenderer
    {
        public rint textReference;
        public rint fontReference;
        public rint materialReference;
        public rint cameraReference;

        public IsTextRenderer(rint textReference, rint fontReference, rint materialReference, rint cameraReference)
        {
            this.textReference = textReference;
            this.fontReference = fontReference;
            this.materialReference = materialReference;
            this.cameraReference = cameraReference;
        }
    }
}