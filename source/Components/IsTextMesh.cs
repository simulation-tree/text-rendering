namespace Rendering.Components
{
    public readonly struct IsTextMesh
    {
        public readonly uint version;

        public IsTextMesh(uint version)
        {
            this.version = version;
        }

        public readonly IsTextMesh IncrementVersion()
        {
            return new(version + 1);
        }
    }
}