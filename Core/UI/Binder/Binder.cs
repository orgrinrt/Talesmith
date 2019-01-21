using GDMechanic.Wiring;
using Godot;
using Talesmith.Core.Systems;
using Talesmith.Core.UI.Binder.BinderContentPages;
using Talesmith.Core.UI.Workspaces;
using Talesmith.Core.UI.Workspaces.Aetas;
using Talesmith.Core.UI.Workspaces.Atlas;
using Talesmith.Core.UI.Workspaces.Home;
using Talesmith.Core.UI.Workspaces.Studia;
using Talesmith.Core.UI.Workspaces.WorldConfig;
using Talesmith.Core.Utils;

namespace Talesmith.Core.UI.Binder
{
    public class Binder : Control
    {
        public override void _Ready()
        {
            this.Wire();
        
            CallDeferred(nameof(DeferredInit));
        }

        private void OpenBinderContentTab(Workspace workspace, WorkspaceChangeType changeType)
        {
            HideBinderContentTabs();

            switch (workspace)
            {
                case HomeWorkspace _:
                    GetHomeContent().Show();
                    break;
                case StudiaWorkspace _:
                    GetStudiaContent().Show();
                    break;
                case AtlasWorkspace _:
                    GetAtlasContent().Show();
                    break;
                case AetasWorkspace _:
                    GetAetasContent().Show();
                    break;
                case ConfigWorkspace _:
                    GetConfigContent().Show();
                    break;
            }
        }
        
        private void HideBinderContentTabs()
        {
            foreach (Node node in GetContentContainer().GetChildren())
            {
                if (node is BinderContentPage page)
                {
                    page.Hide();
                }
            }
        }

        private void ToggleBinder()
        {
        
        }
    
        private void DeferredInit()
        {
            App.Self.Connect(nameof(App.WorkspaceChangeInitiated), this, nameof(OpenBinderContentTab));
            App.Self.Preferences.Connect(nameof(Preferences.BinderToggled), this, nameof(ToggleBinder));
        }

        private Control GetContentContainer()
        {
            return GetNode<Control>("./Content");
        }
        
        private Home GetHomeContent()
        {
            return GetContentContainer().GetNode<Home>("./Home");
        }
        
        private Studia GetStudiaContent()
        {
            return GetContentContainer().GetNode<Studia>("./Studia");
        }
        
        private Atlas GetAtlasContent()
        {
            return GetContentContainer().GetNode<Atlas>("./Atlas");
        }

        private Aetas GetAetasContent()
        {
            return GetContentContainer().GetNode<Aetas>("./Aetas");
        }
        
        private Config GetConfigContent()
        {
            return GetContentContainer().GetNode<Config>("./Config");
        }
    }
}