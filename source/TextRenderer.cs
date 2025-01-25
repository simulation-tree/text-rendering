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
                IsTextRenderer component = entity.GetComponent<IsTextRenderer>();
                uint materialEntity = entity.GetReference(component.materialReference);
                return new(entity.world, materialEntity);
            }
            set
            {
                ref IsTextRenderer component = ref entity.GetComponent<IsTextRenderer>();
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
                IsTextRenderer component = entity.GetComponent<IsTextRenderer>();
                uint meshEntity = entity.GetReference(component.textMeshReference);
                return new Entity(entity.world, meshEntity).As<TextMesh>();
            }
            set
            {
                ref IsTextRenderer component = ref entity.GetComponent<IsTextRenderer>();
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

        public readonly ref LayerMask RenderMask => ref entity.GetComponent<IsTextRenderer>().renderMask;

        readonly uint IEntity.Value => entity.value;
        readonly World IEntity.World => entity.world;

        readonly void IEntity.Describe(ref Archetype archetype)
        {
            archetype.AddComponentType<IsTextRenderer>();
        }

        public TextRenderer(World world, TextMesh textMesh, Material material, LayerMask renderMask)
        {
            entity = new Entity<IsTextRenderer>(world, new IsTextRenderer((rint)1, (rint)2, (rint)3, renderMask));
            entity.AddReference(textMesh);
            entity.AddReference(material);
            entity.AddReference(textMesh.Font);
        }

        public TextRenderer(World world, TextMesh textMesh, Material material) :
            this(world, textMesh, material, LayerMask.All)
        {
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