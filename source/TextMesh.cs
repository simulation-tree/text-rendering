using Fonts;
using Meshes;
using Meshes.Components;
using Rendering.Components;
using Simulation;
using System;
using Unmanaged;
using Unmanaged.Collections;

namespace Rendering
{
    public readonly struct TextMesh : IEntity, IDisposable
    {
        private readonly Mesh mesh;

        /// <summary>
        /// Read only access to the text content.
        /// </summary>
        public readonly ReadOnlySpan<char> Text => ((Entity)mesh).GetList<char>().AsSpan();

        eint IEntity.Value => (Entity)mesh;
        World IEntity.World => (Entity)mesh;

        public TextMesh(World world, eint existingEntity)
        {
            mesh = new(world, existingEntity);
        }

        public TextMesh(World world, ReadOnlySpan<char> text, Font font)
        {
            mesh = new(world);
            Entity entity = mesh;
            rint fontReference = entity.AddReference(font);
            entity.AddComponent(new IsTextMeshRequest(fontReference));

            UnmanagedList<char> list = entity.CreateList<char>();
            list.AddRange(text);
        }

        Query IEntity.GetQuery(World world)
        {
            return new(world, [RuntimeType.Get<IsTextMesh>(), RuntimeType.Get<IsMesh>()]);
        }

        public readonly void Dispose()
        {
            mesh.Dispose();
        }

        /// <summary>
        /// Assigns the text content to the entity.
        /// </summary>
        public readonly void SetText(ReadOnlySpan<char> text)
        {
            UnmanagedList<char> list = ((Entity)mesh).GetList<char>();
            list.Clear();
            list.AddRange(text);

            ref IsTextMeshRequest request = ref ((Entity)mesh).TryGetComponentRef<IsTextMeshRequest>(out bool contains);
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