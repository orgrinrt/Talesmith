using Godot;

namespace Talesmith.Core.LocalData
{
    public class Project : Node
    {
        public static Project Self;
    
        public override void _Ready()
        {
            if (Self == null)
            {
                Self = this;
            }
        }
    }
}
