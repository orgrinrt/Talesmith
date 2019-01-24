using Godot;
using Talesmith.Core.Constants;
using Talesmith.Core.LocalData;
using Talesmith.Core.Systems;
using Talesmith.Core.UI.Binder;
using Talesmith.Core.UI.Dock;
using Talesmith.Core.UI.Inspector;
using Talesmith.Core.UI.Workspaces;

namespace Talesmith.Core
{
    public class App : Node
    {
        [Signal] public delegate void WorkspaceChangeInitiated(Workspace workspace, WorkspaceChangeType changeType);
        [Signal] public delegate void WorkspaceChanged(Workspace workspace);
        
        public static App Self;
        
        public WorkspaceController WorkspaceController => GetWorkspaceController();
        public Project Project => GetProject();
        public Preferences Preferences => GetPreferences();

        public Inspector Inspector => GetInspector();
        public Binder Binder => GetBinder();
        public Dock Dock => GetDock();
        
        public override void _Ready()
        {
            if (Self == null)
            {
                Self = this;
            }
        }

        public void LoadTheme(Resource newThemeSet)
        {
            Preferences.LoadTheme(newThemeSet);
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

        public Binder GetBinder()
        {
            return GetNode<Binder>(NodePaths.Binder);
        }

        public Dock GetDock()
        {
            return GetNode<Dock>(NodePaths.Dock);
        }
    }
}
