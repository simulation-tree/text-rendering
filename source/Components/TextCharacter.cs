namespace Rendering.Components
{
    public struct TextCharacter
    {
        public char value;

        public TextCharacter(char value)
        {
            this.value = value;
        }

        public static implicit operator TextCharacter(char value) => new(value);
        public static implicit operator char(TextCharacter value) => value.value;
    }
}