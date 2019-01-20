using Godot;
using Talesmith.Core.Constants;

namespace Talesmith.Core.UI.Pages
{
    public class Workspace : Control
    {
        internal WorkspaceEnum _workspaceEnum;
        
        public WorkspaceEnum WorkspaceEnum => _workspaceEnum;
        
        public override void _Ready()
        {
            base._Ready();
            
            AddToGroup(Groups.Pages);
        }
    }
}