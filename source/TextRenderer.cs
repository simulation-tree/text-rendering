using Rendering.Components;
using Worlds;

namespace Rendering
{
    public readonly struct TextRenderer : IEntity
    {
        private readonly Entity entity;

        public readonly Material Material
        {
            get
            {
                IsTextRenderer component = entity.GetComponentRef<IsTextRenderer>();
                uint materialEntity = entity.GetReference(component.materialReference);
                return new(entity.world, materialEntity);
            }
            set
            {
                ref IsTextRenderer component = ref entity.GetComponentRef<IsTextRenderer>();
                if (entity.ContainsReference(component.materialReference))
                {
                    entity.SetReference(component.materialReference, value);
                }
                else
                {
                    component.materialReference = entity.AddReference(value);
                }
            }
        }

        public readonly TextMesh TextMesh
        {
            get
            {
                IsTextRenderer component = entity.GetComponentRef<IsTextRenderer>();
                uint meshEntity = entity.GetReference(component.textMeshReference);
                return new Entity(entity.world, meshEntity).As<TextMesh>();
            }
            set
            {
                ref IsTextRenderer component = ref entity.GetComponentRef<IsTextRenderer>();
                if (entity.ContainsReference(component.textMeshReference))
                {
                    entity.SetReference(component.textMeshReference, value);
                }
                else
                {
                    component.textMeshReference = entity.AddReference(value);
                }

                if (entity.ContainsReference(component.fontReference))
                {
                    entity.SetReference(component.fontReference, value.Font);
                }
                else
                {
                    component.fontReference = entity.AddReference(value.Font);
                }
            }
        }

        public readonly ref uint Mask => ref entity.GetComponentRef<IsTextRenderer>().mask;

        readonly uint IEntity.Value => entity.value;
        readonly World IEntity.World => entity.world;
        readonly Definition IEntity.Definition => new([ComponentType.Get<IsTextRenderer>()], []);

        public TextRenderer(World world, uint existingEntity)
        {
            entity = new(world, existingEntity);
        }

        public TextRenderer(World world, TextMesh textMesh, Material material, uint mask = 1)
        {
            entity = new Entity(world);
            rint textMeshReference = entity.AddReference(textMesh);
            rint materialReference = entity.AddReference(material);
            rint fontReference = entity.AddReference(textMesh.Font);
            entity.AddComponent(new IsTextRenderer(textMeshReference, materialReference, fontReference, mask));
        }

        public readonly void Dispose()
        {
            entity.Dispose();
        }

        public static implicit operator Entity(TextRenderer renderer)
        {
            return renderer.entity;
        }
    }
}