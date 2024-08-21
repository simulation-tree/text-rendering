using Fonts;
using Meshes;
using Meshes.Components;
using Rendering.Components;
using Simulation;
using System;
using Unmanaged;

namespace Rendering
{
    public readonly struct TextMesh : IEntity, IDisposable
    {
        private readonly Mesh mesh;

        eint IEntity.Value => (Entity)mesh;
        World IEntity.World => (Entity)mesh;

        public TextMesh(World world, eint existingEntity)
        {
            mesh = new(world, existingEntity);
        }

        public TextMesh(World world, Text text, Font font)
        {
            mesh = new(world);
            Entity entity = mesh;
            rint textReference = entity.AddReference(text);
            rint fontReference = entity.AddReference(font);
            entity.AddComponent(new IsTextMeshRequest(textReference, fontReference));
        }

        Query IEntity.GetQuery(World world)
        {
            return new(world, [RuntimeType.Get<IsTextMesh>(), RuntimeType.Get<IsMesh>()]);
        }

        public readonly void Dispose()
        {
            mesh.Dispose();
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