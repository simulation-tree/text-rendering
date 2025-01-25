using Types;
using Worlds;
using Worlds.Tests;

namespace TextRendering.Tests
{
    public abstract class TextRenderingTests : WorldTests
    {
        static TextRenderingTests()
        {
            TypeRegistry.Load<TextRendering.TypeBank>();
        }

        protected override Worlds.Schema CreateSchema()
        {
            Schema schema = base.CreateSchema();
            schema.Load<TextRendering.SchemaBank>();
            return schema;
        }
    }
}