using System;

namespace Rendering.Components
{
    public struct IsTextMesh
    {
        public uint version;

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
    }
}