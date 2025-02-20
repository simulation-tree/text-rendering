using Fonts;
using Rendering;
using Unmanaged;
using Worlds;

namespace TextRendering.Tests
{
    public class TextMeshTests : TextRenderingTests
    {
        [Test]
        public void VerifyCompliance()
        {
            using World world = CreateWorld();
            Font font = new(world, "Assets/SomeFont.ttf");
            TextMesh a = new(world, "Aye", font);
            Assert.That(a.IsLoaded, Is.False); //needs a system to generate the text
        }

        [Test]
        public void UpdatingTextMeshContent()
        {
            using World world = CreateWorld();
            Font font = new(world, "Assets/SomeFont.ttf");

            FixedString content = "Hammer time";
            TextMesh textMesh = new(world, content, font);

            Assert.That(textMesh.Content.ToString(), Is.EqualTo(content.ToString()));

            content = "Smooth operator";
            textMesh.SetText(content);

            Assert.That(textMesh.Content.ToString(), Is.EqualTo(content.ToString()));
        }
    }
}