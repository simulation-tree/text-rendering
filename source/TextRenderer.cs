using Rendering.Components;
using Simulation;
using Unmanaged;

namespace Rendering
{
    public readonly struct TextRenderer : IEntity
    {
        public readonly Entity entity;

        public readonly bool IsEnabled
        {
            get => entity.IsEnabled;
            set => entity.IsEnabled = value;
        }

        public readonly Entity Parent
        {
            get => entity.Parent;
            set => entity.Parent = value;
        }

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
                uint meshEntity = entity.GetReference(component.meshReference);
                return new Entity(entity.world, meshEntity).As<TextMesh>();
            }
            set
            {
                ref IsTextRenderer component = ref entity.GetComponentRef<IsTextRenderer>();
                if (entity.ContainsReference(component.meshReference))
                {
                    entity.SetReference(component.meshReference, value);
                }
                else
                {
                    component.meshReference = entity.AddReference(value);
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

        public readonly Camera Camera
        {
            get
            {
                IsTextRenderer component = entity.GetComponentRef<IsTextRenderer>();
                uint cameraEntity = entity.GetReference(component.cameraReference);
                return new(entity.world, cameraEntity);
            }
            set
            {
                ref IsTextRenderer component = ref entity.GetComponentRef<IsTextRenderer>();
                if (entity.ContainsReference(component.cameraReference))
                {
                    entity.SetReference(component.cameraReference, value);
                }
                else
                {
                    component.cameraReference = entity.AddReference(value);
                }
            }
        }

        readonly uint IEntity.Value => entity.value;
        readonly World IEntity.World => entity.world;
        readonly Definition IEntity.Definition => new([RuntimeType.Get<IsTextRenderer>()], []);

        public TextRenderer(World world, uint existingEntity)
        {
            entity = new(world, existingEntity);
        }

        public TextRenderer(World world, TextMesh mesh, Material material, Camera camera)
        {
            entity = new Entity(world);
            rint meshReference = entity.AddReference(mesh);
            rint materialReference = entity.AddReference(material);
            rint cameraReference = entity.AddReference(camera);
            rint fontReference = entity.AddReference(mesh.Font);
            entity.AddComponent(new IsTextRenderer(meshReference, materialReference, cameraReference, fontReference));
        }
    }
}