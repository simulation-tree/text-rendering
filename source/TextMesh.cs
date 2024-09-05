using Fonts;
using Meshes;
using Meshes.Components;
using Rendering.Components;
using Simulation;
using System.Numerics;
using Unmanaged;

namespace Rendering
{
    public readonly struct TextMesh : IEntity
    {
        public readonly Mesh mesh;

        /// <summary>
        /// Read only access to the text content.
        /// </summary>
        public readonly USpan<char> Text => mesh.entity.GetArray<char>();

        public readonly Font Font
        {
            get
            {
                IsTextMeshRequest request = mesh.entity.GetComponentRef<IsTextMeshRequest>();
                rint fontReference = request.fontReference;
                uint fontEntity = mesh.entity.GetReference(fontReference);
                return new Font(mesh.entity.world, fontEntity);
            }
        }

        public readonly Vector2 Alignment
        {
            get
            {
                IsTextMeshRequest request = mesh.entity.GetComponentRef<IsTextMeshRequest>();
                return request.alignment;
            }
            set
            {
                ref IsTextMeshRequest request = ref mesh.entity.GetComponentRef<IsTextMeshRequest>();
                request.alignment = value;
                request.version++;
            }
        }

        readonly uint IEntity.Value => mesh.entity.value;
        readonly World IEntity.World => mesh.entity.world;
        readonly Definition IEntity.Definition => new([RuntimeType.Get<IsTextMesh>(), RuntimeType.Get<IsMesh>()], []);

        public TextMesh(World world, USpan<char> text, Font font, Vector2 alignment = default)
        {
            Entity entity = new(world);
            mesh = entity.As<Mesh>();
            rint fontReference = entity.AddReference(font);
            entity.AddComponent(new IsTextMeshRequest(fontReference, alignment));
            entity.CreateArray(text);
        }

        public TextMesh(World world, FixedString text, Font font, Vector2 alignment = default)
        {
            Entity entity = new(world);
            mesh = entity.As<Mesh>();
            rint fontReference = entity.AddReference(font);
            entity.AddComponent(new IsTextMeshRequest(fontReference, alignment));
            USpan<char> buffer = stackalloc char[(int)FixedString.MaxLength];
            uint length = text.CopyTo(buffer);
            entity.CreateArray(buffer.Slice(0, length));
        }

        public TextMesh(World world, string text, Font font, Vector2 alignment = default)
        {
            Entity entity = new(world);
            mesh = entity.As<Mesh>();
            rint fontReference = entity.AddReference(font);
            entity.AddComponent(new IsTextMeshRequest(fontReference, alignment));
            entity.CreateArray(text.AsSpan());
        }

        /// <summary>
        /// Assigns the text content to the entity.
        /// </summary>
        public readonly void SetText(USpan<char> text)
        {
            USpan<char> array = mesh.entity.ResizeArray<char>(text.length);
            text.CopyTo(array);
            ref IsTextMeshRequest request = ref mesh.entity.TryGetComponentRef<IsTextMeshRequest>(out bool contains);
            if (contains)
            {
                request.version++;
            }
        }

        public readonly void SetText(FixedString text)
        {
            USpan<char> buffer = stackalloc char[(int)FixedString.MaxLength];
            uint length = text.CopyTo(buffer);
            SetText(buffer.Slice(0, length));
        }

        public readonly void SetText(string text)
        {
            SetText(text.AsSpan());
        }
    }
}