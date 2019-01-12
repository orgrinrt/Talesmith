using Godot;

namespace Talesmith.Core.Utils
{
    public class MathHelper
    {
        public static double RadianToDegree(double angle)
        {
            return angle * (180.0 / Mathf.Pi);
        }
        
        public static double DegreeToRadian(double angle)
        {
            return Mathf.Pi * angle / 180.0;
        }

        public static float Normalize(float raw, float min, float max)
        {
            return (raw - min) / (max - min);
        }
        
        public static double GetAngleDegree2D(Vector2 origin, Vector2 target) {
            var n = 270 - (Mathf.Atan2(origin.y - target.y, origin.x - target.x)) * 180 / Mathf.Pi;
            return n % 360;
        }

        public static Vector2 TexturePixelSizeToScale(Vector2 sourceSize, int targetHeight, int targetWidth)
        {
            return new Vector2(sourceSize.x / (sourceSize.x / targetWidth) / 250, (sourceSize.y / (sourceSize.y / targetHeight)) / 250);
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            /*
            return (Mathf.Pow(a.x-b.x,2)+Mathf.Pow(a.y-b.y,2));
            */
            return a.DistanceTo(b);
        }

        public static float Percentage(float input, float min, float max)
        {
            return ((input - min) * 100) / (max - min);
        }
        
        public static float PercentageAsDecimal(float input, float min, float max)
        {
            return (((input - min) * 100) / (max - min)) / 100;
        }
    }
}