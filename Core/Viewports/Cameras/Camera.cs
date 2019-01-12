using Godot;
using Talesmith.Core.Constants;

namespace Talesmith.Core.Viewports.Cameras
{
    public class Camera : Camera2D
    {
        public override void _Ready()
        {
            AddToGroup(Groups.Cameras);
        }
    }
}