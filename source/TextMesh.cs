using Fonts;
using Meshes;
using Meshes.Components;
using Rendering.Components;
using System;
using Unmanaged;
using Worlds;

namespace Rendering
{
    public readonly struct TextMesh : IEntity
    {
        private readonly Mesh mesh;

        /// <summary>
        /// Read only access to the text content.
        /// </summary>
        public readonly USpan<char> Text => mesh.AsEntity().GetArray<TextCharacter>().As<char>();

        public readonly Font Font
        {
            get
            {
                IsTextMeshRequest request = mesh.AsEntity().GetComponent<IsTextMeshRequest>();
                rint fontReference = request.fontReference;
                uint fontEntity = mesh.GetReference(fontReference);
                return new Font(mesh.AsEntity().world, fontEntity);
            }
            set
            {
                ref IsTextMeshRequest request = ref mesh.AsEntity().GetComponent<IsTextMeshRequest>();
                ref rint fontReference = ref request.fontReference;
                if (fontReference == default)
                {
                    fontReference = mesh.AddReference(value);
                }
                else
                {
                    mesh.SetReference(fontReference, value);
                }
            }
        }

        readonly uint IEntity.Value => mesh.GetEntityValue();
        readonly World IEntity.World => mesh.GetWorld();

        readonly void IEntity.Describe(ref Archetype archetype)
        {
            archetype.AddComponentType<IsTextMesh>();
            archetype.AddComponentType<IsMesh>();
        }

        public TextMesh(World world, uint existingEntity)
        {
            mesh = new(world, existingEntity);
        }

        public TextMesh(World world, USpan<char> text, Font font)
        {
            Entity entity = new(world);
            mesh = entity.As<Mesh>();
            rint fontReference = entity.AddReference(font);
            entity.AddComponent(new IsTextMeshRequest(fontReference));
            entity.CreateArray(text.As<TextCharacter>());
        }

        public TextMesh(World world, FixedString text, Font font)
        {
            Entity entity = new(world);
            mesh = entity.As<Mesh>();
            rint fontReference = entity.AddReference(font);
            entity.AddComponent(new IsTextMeshRequest(fontReference));
            USpan<char> buffer = stackalloc char[(int)FixedString.Capacity];
            uint length = text.CopyTo(buffer);
            entity.CreateArray(buffer.Slice(0, length).As<TextCharacter>());
        }

        public TextMesh(World world, string text, Font font)
        {
            Entity entity = new(world);
            mesh = entity.As<Mesh>();
            rint fontReference = entity.AddReference(font);
            entity.AddComponent(new IsTextMeshRequest(fontReference));
            entity.CreateArray(new USpan<char>(text).As<TextCharacter>());
        }

        public readonly void Dispose()
        {
            mesh.Dispose();
        }

        /// <summary>
        /// Assigns the text content to the entity.
        /// </summary>
        public readonly void SetText(USpan<char> text)
        {
            USpan<TextCharacter> array = mesh.AsEntity().ResizeArray<TextCharacter>(text.Length);
            text.As<TextCharacter>().CopyTo(array);
            ref IsTextMeshRequest request = ref mesh.AsEntity().TryGetComponent<IsTextMeshRequest>(out bool contains);
            if (contains)
            {
                request.version++;
            }
        }

        public readonly void SetText(string text)
        {
            SetText(text.AsSpan());
        }

        public readonly void SetText(FixedString text)
        {
            USpan<char> buffer = stackalloc char[(int)text.Length];
            uint length = text.CopyTo(buffer);
            SetText(buffer.Slice(0, length));
        }

        public static implicit operator Entity(TextMesh textMesh)
        {
            return textMesh.mesh.AsEntity();
        }

        public static implicit operator Mesh(TextMesh textMesh)
        {
            return textMesh.mesh;
        }
    }
}