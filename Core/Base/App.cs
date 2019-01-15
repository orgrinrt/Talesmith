using Godot;
using Talesmith.Core.Constants;
using Talesmith.Core.LocalData;
using Talesmith.Core.UI.Pages;

namespace Talesmith.Core
{
    public class App : Node
    {
        [Signal] public delegate void MainPageChanged(Page page);
        
        public static App Self;
        
        public PageController PageController => GetPageController();
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

        public PageController GetPageController()
        {
            return GetProject().GetNode<PageController>("./UI/Pages");
        }

        public Preferences GetPreferences()
        {
            return GetNode<Preferences>(NodePaths.Preferences);
        }
    }
}
