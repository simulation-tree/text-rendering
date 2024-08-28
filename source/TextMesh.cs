using Fonts;
using Meshes;
using Meshes.Components;
using Rendering.Components;
using Simulation;
using System;
using System.Numerics;
using Unmanaged;

namespace Rendering
{
    public readonly struct TextMesh : IEntity
    {
        private readonly Mesh mesh;

        /// <summary>
        /// Read only access to the text content.
        /// </summary>
        public readonly ReadOnlySpan<char> Text => ((Entity)mesh).GetArray<char>();

        public readonly Font Font
        {
            get
            {
                Entity entity = mesh;
                IsTextMeshRequest request = entity.GetComponent<IsTextMeshRequest>();
                rint fontReference = request.fontReference;
                eint fontEntity = entity.GetReference(fontReference);
                return new Font(entity, fontEntity);
            }
        }

        public readonly Vector2 Alignment
        {
            get
            {
                Entity entity = mesh;
                IsTextMeshRequest request = entity.GetComponent<IsTextMeshRequest>();
                return request.alignment;
            }
            set
            {
                Entity entity = mesh;
                ref IsTextMeshRequest request = ref entity.GetComponent<IsTextMeshRequest>();
                request.alignment = value;
                request.version++;
            }
        }

        eint IEntity.Value => (Entity)mesh;
        World IEntity.World => (Entity)mesh;

        public TextMesh(World world, ReadOnlySpan<char> text, Font font, Vector2 alignment = default)
        {
            Entity entity = new(world);
            mesh = entity.As<Mesh>();
            rint fontReference = entity.AddReference(font);
            entity.AddComponent(new IsTextMeshRequest(fontReference, alignment));
            entity.CreateArray(text);
        }

        Query IEntity.GetQuery(World world)
        {
            return new(world, [RuntimeType.Get<IsTextMesh>(), RuntimeType.Get<IsMesh>()]);
        }

        /// <summary>
        /// Assigns the text content to the entity.
        /// </summary>
        public readonly void SetText(ReadOnlySpan<char> text)
        {
            Entity entity = mesh;
            Span<char> array = entity.ResizeArray<char>((uint)text.Length);
            text.CopyTo(array);
            ref IsTextMeshRequest request = ref entity.TryGetComponentRef<IsTextMeshRequest>(out bool contains);
            if (contains)
            {
                request.version++;
            }
        }

        public static implicit operator Mesh(TextMesh mesh)
        {
            return mesh.mesh;
        }

        public static implicit operator Entity(TextMesh mesh)
        {
            return mesh.mesh;
        }
    }
}