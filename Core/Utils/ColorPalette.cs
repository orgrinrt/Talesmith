using Godot;

namespace Taleteller.Core.Utils
{
    public class ColorPalette
    {
        public static Godot.Color Red = new Color(0.5f, 0.0f, 0.0f);
        public static Godot.Color Blue = new Color(0.0f, 0.0f, 0.5f);
        public static Godot.Color Green = new Color(0.0f, 0.5f, 0.0f);
        public static Godot.Color Black = new Color(0.0f, 0.0f, 0.0f);
        public static Godot.Color White = new Color(1f, 1f, 1f);

        public static float GrayScale(Color source)
        {
            return (source.r + source.g + source.b) / 3;
        }
    }
}