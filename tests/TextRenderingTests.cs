using Fonts;
using Meshes;
using Types;
using Worlds;
using Worlds.Tests;

namespace TextRendering.Tests
{
    public abstract class TextRenderingTests : WorldTests
    {
        static TextRenderingTests()
        {
            TypeRegistry.Load<TextRenderingTypeBank>();
            TypeRegistry.Load<MeshesTypeBank>();
            TypeRegistry.Load<FontsTypeBank>();
        }

        protected override Schema CreateSchema()
        {
            Schema schema = base.CreateSchema();
            schema.Load<TextRenderingSchemaBank>();
            schema.Load<MeshesSchemaBank>();
            schema.Load<FontsSchemaBank>();
            return schema;
        }
    }
}