using Godot;
using Talesmith.Core.Constants;

namespace Talesmith.Core.UI.Workspaces
{
    public class Workspace : Control, ICycleableItem
    {
        public ICycleableItem NextItem => GetNextItem();
        public ICycleableItem PrevItem => GetPrevItem();
        
        public override void _Ready()
        {
            base._Ready();
            
            SetFocusMode(FocusModeEnum.All);
            AddToGroup(Groups.Workspaces);

            int margin = (int)App.Self.Preferences.AppearancePreferences.Get("ui_base_margin");
            MarginRight = -margin;
            MarginBottom = -margin;;
            MarginLeft = margin;
            MarginTop = margin;
        }

        internal WorkspaceController GetWorkspaceController()
        {
            return GetNode<WorkspaceController>("..");
        }

        public virtual ICycleableItem GetNextItem()
        {
            throw new System.NotImplementedException();
        }

        public virtual ICycleableItem GetPrevItem()
        {
            throw new System.NotImplementedException();
        }
    }
}