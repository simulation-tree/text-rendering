using Fonts;
using Rendering.Components;
using Simulation;
using System;
using Unmanaged;

namespace Rendering
{
    public readonly struct TextRenderer : IEntity, IDisposable
    {
        private readonly Entity entity;

        public readonly bool IsEnabled
        {
            get => entity.IsEnabled;
            set => entity.IsEnabled = value;
        }

        public readonly Material Material
        {
            get
            {
                IsTextRenderer component = entity.GetComponent<IsTextRenderer>();
                eint materialEntity = entity.GetReference(component.materialReference);
                return new(entity, materialEntity);
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

        public readonly Camera Camera
        {
            get
            {
                IsTextRenderer component = entity.GetComponent<IsTextRenderer>();
                eint cameraEntity = entity.GetReference(component.cameraReference);
                return new(entity, cameraEntity);
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

        public readonly Text Text
        {
            get
            {
                IsTextRenderer component = entity.GetComponent<IsTextRenderer>();
                eint textEntity = entity.GetReference(component.textReference);
                return new(entity, textEntity);
            }
            set
            {
                ref IsTextRenderer component = ref entity.GetComponentRef<IsTextRenderer>();
                if (entity.ContainsReference(component.textReference))
                {
                    entity.SetReference(component.textReference, value);
                }
                else
                {
                    component.textReference = entity.AddReference(value);
                }
            }
        }

        public readonly Font Font
        {
            get
            {
                IsTextRenderer component = entity.GetComponent<IsTextRenderer>();
                eint fontEntity = entity.GetReference(component.fontReference);
                return new(entity, fontEntity);
            }
            set
            {
                ref IsTextRenderer component = ref entity.GetComponentRef<IsTextRenderer>();
                if (entity.ContainsReference(component.fontReference))
                {
                    entity.SetReference(component.fontReference, value);
                }
                else
                {
                    component.fontReference = entity.AddReference(value);
                }
            }
        }

        readonly eint IEntity.Value => entity;
        readonly World IEntity.World => entity;

        public TextRenderer(World world, eint existingEntity)
        {
            entity = new(world, existingEntity);
        }

        public TextRenderer(World world, Text text, Font font, Material material, Camera camera)
        {
            entity = new(world);
            rint textReference = entity.AddReference(text);
            rint fontReference = entity.AddReference(font);
            rint materialReference = entity.AddReference(material);
            rint cameraReference = entity.AddReference(camera);
            entity.AddComponent(new IsTextRenderer(textReference, fontReference, materialReference, cameraReference));
        }

        public readonly void Dispose()
        {
            entity.Dispose();
        }

        readonly Query IEntity.GetQuery(World world)
        {
            return new(world, RuntimeType.Get<IsTextRenderer>());
        }
    }
}