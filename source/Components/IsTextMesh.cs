using System;

namespace Rendering.Components
{
    public readonly struct IsTextMesh
    {
        public readonly uint version;

#if NET
        [Obsolete("Default constructor not supported", true)]
        public IsTextMesh()
        {
            throw new NotSupportedException("Default constructor not supported");
        }
#endif

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