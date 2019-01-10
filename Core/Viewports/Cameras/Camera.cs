using Godot;
using Taleteller.Core.Constants;

namespace Taleteller.Core.Viewports.Cameras
{
    public class Camera : Camera2D
    {
        public override void _Ready()
        {
            AddToGroup(Groups.Cameras);
        }
    }
}