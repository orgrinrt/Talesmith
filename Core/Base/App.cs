using Godot;
using Talesmith.Core.Constants;
using Talesmith.Core.LocalData;
using Talesmith.Core.UI.Inspector;
using Talesmith.Core.UI.Pages;

namespace Talesmith.Core
{
    public class App : Node
    {
        [Signal] public delegate void MainPageChanged(Workspace workspace);
        
        public static App Self;
        
        public WorkspaceController WorkspaceController => GetWorkspaceController();
        public Project Project => GetProject();
        public Preferences Preferences => GetPreferences();
        
        public override void _Ready()
        {
            if (Self == null)
            {
                Self = this;
            }
        }
        
        public Project GetProject()
        {
            Node top = GetTree().GetRoot();
            return top.GetChild(top.GetChildCount() - 1) as Project;
        }

        public WorkspaceController GetWorkspaceController()
        {
            return GetProject().GetNode<WorkspaceController>("./UI/Workspaces");
        }

        public Preferences GetPreferences()
        {
            return GetNode<Preferences>(NodePaths.Preferences);
        }

        public Inspector GetInspector()
        {
            return GetNode<Inspector>(NodePaths.Inspector);
        }

        public Dock GetDock()
        {
            return GetNode<Dock>(NodePaths.Dock);
        }
    }
}
