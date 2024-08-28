using Rendering.Components;
using Simulation;
using Unmanaged;

namespace Rendering
{
    public readonly struct TextRenderer : IEntity
    {
        private readonly Renderer renderer;

        public readonly bool IsEnabled
        {
            get => renderer.IsEnabled;
            set => renderer.IsEnabled = value;
        }

        public readonly Entity Parent
        {
            get => renderer.Parent;
            set => renderer.Parent = value;
        }

        eint IEntity.Value => (Entity)renderer;
        World IEntity.World => (Entity)renderer;

        public TextRenderer(World world, eint existingEntity)
        {
            renderer = new(world, existingEntity);
        }

        public TextRenderer(World world, TextMesh mesh, Material material, Camera camera)
        {
            Entity entity = new(world);
            renderer = entity.As<Renderer>();
            rint meshReference = entity.AddReference(mesh);
            rint materialReference = entity.AddReference(material);
            rint cameraReference = entity.AddReference(camera);
            rint fontReference = entity.AddReference(mesh.Font);
            entity.AddComponent(new IsTextRenderer(meshReference, materialReference, cameraReference, fontReference));
        }

        Query IEntity.GetQuery(World world)
        {
            return new(world, [RuntimeType.Get<IsTextRenderer>(), RuntimeType.Get<IsRenderer>()]);
        }

        public static implicit operator Entity(TextRenderer renderer)
        {
            return renderer.renderer;
        }

        public static implicit operator Renderer(TextRenderer renderer)
        {
            return renderer.renderer;
        }
    }
}