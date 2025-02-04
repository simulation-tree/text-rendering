using Fonts;
using Meshes;
using Meshes.Components;
using Rendering.Components;
using System;
using Unmanaged;
using Worlds;

namespace Rendering
{
    public readonly partial struct TextMesh : IEntity
    {
        /// <summary>
        /// Read only access to the text content.
        /// </summary>
        public readonly USpan<char> Text => GetArray<TextCharacter>().As<char>();

        public readonly Font Font
        {
            get
            {
                ref IsTextMeshRequest component = ref GetComponent<IsTextMeshRequest>();
                rint fontReference = component.fontReference;
                uint fontEntity = GetReference(fontReference);
                return new Entity(world, fontEntity).As<Font>();
            }
            set
            {
                ref IsTextMeshRequest request = ref GetComponent<IsTextMeshRequest>();
                ref rint fontReference = ref request.fontReference;
                if (fontReference == default)
                {
                    fontReference = AddReference(value);
                }
                else
                {
                    SetReference(fontReference, value);
                }
            }
        }

        public readonly bool IsLoaded
        {
            get
            {
                if (TryGetComponent(out IsTextMeshRequest request))
                {
                    return request.loaded;
                }

                return IsCompliant;
            }
        }

        public TextMesh(World world, USpan<char> text, Font font)
        {
            this.world = world;
            value = world.CreateEntity(new IsTextMeshRequest((rint)1));
            AddReference(font);
            CreateArray(text.As<TextCharacter>());
        }

        public TextMesh(World world, FixedString text, Font font)
        {
            this.world = world;
            value = world.CreateEntity(new IsTextMeshRequest((rint)1));
            AddReference(font);
            USpan<char> buffer = stackalloc char[(int)FixedString.Capacity];
            uint length = text.CopyTo(buffer);
            CreateArray(buffer.Slice(0, length).As<TextCharacter>());
        }

        public TextMesh(World world, string text, Font font)
        {
            this.world = world;
            value = world.CreateEntity(new IsTextMeshRequest((rint)1));
            AddReference(font);
            CreateArray(new USpan<char>(text).As<TextCharacter>());
        }

        readonly void IEntity.Describe(ref Archetype archetype)
        {
            archetype.AddComponentType<IsTextMesh>();
            archetype.AddComponentType<IsMesh>();
        }

        /// <summary>
        /// Assigns the text content to the entity.
        /// </summary>
        public readonly void SetText(USpan<char> text)
        {
            USpan<TextCharacter> array = ResizeArray<TextCharacter>(text.Length);
            text.As<TextCharacter>().CopyTo(array);
            ref IsTextMeshRequest request = ref TryGetComponent<IsTextMeshRequest>(out bool contains);
            if (contains)
            {
                request.loaded = false;
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

        public static implicit operator Mesh(TextMesh textMesh)
        {
            return textMesh.As<Mesh>();
        }
    }
}