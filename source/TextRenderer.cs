using Fonts;
using Materials;
using Rendering.Components;
using Worlds;

namespace Rendering
{
    public readonly partial struct TextRenderer : IEntity
    {
        public readonly Material Material
        {
            get
            {
                IsTextRenderer component = GetComponent<IsTextRenderer>();
                uint materialEntity = GetReference(component.materialReference);
                return new Entity(world, materialEntity).As<Material>();
            }
            set
            {
                ref IsTextRenderer component = ref GetComponent<IsTextRenderer>();
                if (ContainsReference(component.materialReference))
                {
                    SetReference(component.materialReference, value);
                }
                else
                {
                    component.materialReference = AddReference(value);
                }
            }
        }

        public readonly TextMesh TextMesh
        {
            get
            {
                IsTextRenderer component = GetComponent<IsTextRenderer>();
                uint meshEntity = GetReference(component.textMeshReference);
                return new Entity(world, meshEntity).As<TextMesh>();
            }
            set
            {
                ref IsTextRenderer component = ref GetComponent<IsTextRenderer>();
                if (ContainsReference(component.textMeshReference))
                {
                    SetReference(component.textMeshReference, value);
                }
                else
                {
                    component.textMeshReference = AddReference(value);
                }

                if (ContainsReference(component.fontReference))
                {
                    SetReference(component.fontReference, value.Font);
                }
                else
                {
                    component.fontReference = AddReference(value.Font);
                }
            }
        }

        public readonly Font Font
        {
            get
            {
                IsTextRenderer component = GetComponent<IsTextRenderer>();
                uint fontEntity = GetReference(component.fontReference);
                return new Entity(world, fontEntity).As<Font>();
            }
            set
            {
                ref IsTextRenderer component = ref GetComponent<IsTextRenderer>();
                if (ContainsReference(component.fontReference))
                {
                    SetReference(component.fontReference, value);
                }
                else
                {
                    component.fontReference = AddReference(value);
                }
            }
        }

        public readonly ref LayerMask RenderMask => ref GetComponent<IsTextRenderer>().renderMask;


        public TextRenderer(World world, TextMesh textMesh, Material material, LayerMask renderMask)
        {
            this.world = world;
            value = world.CreateEntity(new IsTextRenderer((rint)1, (rint)2, (rint)3, renderMask));
            AddReference(textMesh);
            AddReference(material);
            AddReference(textMesh.Font);
        }

        public TextRenderer(World world, TextMesh textMesh, Material material) :
            this(world, textMesh, material, LayerMask.All)
        {
        }

        readonly void IEntity.Describe(ref Archetype archetype)
        {
            archetype.AddComponentType<IsTextRenderer>();
        }
    }
}