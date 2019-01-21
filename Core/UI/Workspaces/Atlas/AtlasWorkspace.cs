using System;
using Godot;
using Talesmith.Core.Viewports.Cameras;

namespace Talesmith.Core.UI.Workspaces.Atlas
{
    public class AtlasWorkspace : Workspace
    {
        public override void _Ready()
        {
            base._Ready();
        }
        
        // NOTE: We might want to only process FreeCamera input whenever mouse is over this page
        // One way to do this would be to have flag on this page whenever mouse is over and check that whenever processing input in Freecam

        public override void _Notification(int what)
        {
            base._Notification(what);

            switch (what)
            {
                case NotificationVisibilityChanged:
                    if (Visible)
                    {
                        GetAtlas2D().Show();
                        GetWorkspaceController().GetBackgroundPanel().Hide();
                    }
                    else
                    {
                        GetAtlas2D().Hide();
                        GetWorkspaceController().GetBackgroundPanel().Show();
                    }
                    break;
                case NotificationMouseEnter:
                    GrabFocus();
                    GetFreeCamera().SetPanningLocked(false);
                    break;
                case NotificationMouseExit:
                    GetFreeCamera().SetPanningLocked(true);
                    break;
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
        
        public override ICycleableItem GetNextItem()
        {
            return App.Self.WorkspaceController.Aetas;
        }

        public override ICycleableItem GetPrevItem()
        {
            return App.Self.WorkspaceController.Studia;
        }
    }
}
