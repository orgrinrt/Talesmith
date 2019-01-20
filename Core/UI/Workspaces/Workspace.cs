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
            
            AddToGroup(Groups.Pages);
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