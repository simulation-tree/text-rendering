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
        }

        protected override Worlds.Schema CreateSchema()
        {
            Schema schema = base.CreateSchema();
            schema.Load<TextRenderingSchemaBank>();
            return schema;
        }
    }
}