using Rendering.Components;
using Simulation;
using System;
using Unmanaged;

namespace Rendering
{
    public readonly struct Text : IEntity
    {
        private readonly Entity entity;

        public readonly ReadOnlySpan<char> Content => entity.GetList<char>().AsSpan();
        public readonly uint Length => entity.GetListLength<char>();
        public readonly ref char this[uint index] => ref entity.GetListElement<char>(index);

        readonly eint IEntity.Value => entity;
        readonly World IEntity.World => entity;

        public Text(World world, eint existingEntity)
        {
            entity = new(world, existingEntity);
        }

        public Text(World world, ReadOnlySpan<char> content)
        {
            entity = new(world);
            entity.CreateList<char>().AddRange(content);
        }

        public readonly void Dispose()
        {
            entity.Dispose();
        }

        readonly Query IEntity.GetQuery(World world)
        {
            return new(world, RuntimeType.Get<IsText>());
        }

        public static implicit operator Entity(Text text)
        {
            return text.entity;
        }
    }
}