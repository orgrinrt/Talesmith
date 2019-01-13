using System;
using Godot;
using Talesmith.Core.Viewports.Cameras;

namespace Talesmith.Core.UI.Pages.Atlas
{
    public class AtlasPage : Page
    {
        public override void _Ready()
        {
            PageEnum = PageEnum.Atlas;
        }

        public override void _Notification(int what)
        {
            base._Notification(what);

            if (what == NotificationVisibilityChanged)
            {
                if (Visible)
                {
                    GetAtlas2D().Show();
                }
                else
                {
                    GetAtlas2D().Hide();
                }
            }
        }

        public FreeCamera GetFreeCamera()
        {
            return GetNode<FreeCamera>("../../../Atlas2D/FreeCam");
        }
        
        private Node2D GetAtlas2D()
        {
            return GetNode<Node2D>("../../../Atlas2D");
        }
    }
}
