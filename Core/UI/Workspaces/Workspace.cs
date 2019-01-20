using Godot;
using Talesmith.Core.Constants;

namespace Talesmith.Core.UI.Workspaces
{
    public class Workspace : Control, ICycleableItem
    {
        internal WorkspaceEnum _workspaceEnum;
        
        public WorkspaceEnum WorkspaceEnum => _workspaceEnum;
        public ICycleableItem NextItem => GetNextItem();
        public ICycleableItem PrevItem => GetPrevItem();
        
        public override void _Ready()
        {
            base._Ready();
            
            AddToGroup(Groups.Pages);
        }

        public override void _Input(InputEvent @event)
        {
            base._Input(@event);

            if (@event.IsActionPressed("cycle_workspaces_next"))
            {
                
            }
            else if (@event.IsActionPressed("cycle_workspaces_prev"))
            {
                
            }
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