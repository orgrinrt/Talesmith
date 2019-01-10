using Godot;

namespace Taleteller.Core.Utils
{
    public static class Direction
    {
        public static Vector2 Right2D = new Vector2(1,0);
        public static Vector2 Left2D = new Vector2(-1,0);
        public static Vector2 Up2D = new Vector2(0,-1);
        public static Vector2 Down2D = new Vector2(0,1);
        
        public static Vector3 Down = new Vector3(0, -1, 0);
        public static Vector3 Up = new Vector3(0, 1, 0);

        public static Vector2 GetNormalized(Vector2 origin, Vector2 target)
        {
            return (target - origin).Normalized();
        }
    }
}