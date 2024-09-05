using Rendering.Components;
using Simulation;
using Unmanaged;

namespace Rendering
{
    public readonly struct TextRenderer : IEntity
    {
        public readonly Renderer renderer;

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

        readonly uint IEntity.Value => renderer.entity.value;
        readonly World IEntity.World => renderer.entity.world;
        readonly Definition IEntity.Definition => new([RuntimeType.Get<IsTextRenderer>(), RuntimeType.Get<IsRenderer>()], []);

        public TextRenderer(World world, uint existingEntity)
        {
            renderer = new(world, existingEntity);
        }

        public TextRenderer(World world, TextMesh mesh, Material material, Camera camera)
        {
            renderer = new Entity(world).As<Renderer>();
            rint meshReference = renderer.entity.AddReference(mesh);
            rint materialReference = renderer.entity.AddReference(material);
            rint cameraReference = renderer.entity.AddReference(camera);
            rint fontReference = renderer.entity.AddReference(mesh.Font);
            renderer.entity.AddComponent(new IsTextRenderer(meshReference, materialReference, cameraReference, fontReference));
        }
    }
}