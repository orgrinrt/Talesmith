using Godot;
using Talesmith.Core.UI.Pages.Atlas;

namespace Talesmith.Core.UI.Pages
{
    public class PageController : Control
    {
        public override void _Ready()
        {
        
        }

        public void ChangePage(PageEnum page)
        {
            HidePages();

            Page pageToOpen = new Page();
            switch (page)
            {
                case PageEnum.Atlas:
                    pageToOpen = GetAtlasPage();
                    break;
                case PageEnum.WorldConfig:
                    pageToOpen = GetConfigPage();
                    break;
            }

            pageToOpen.Show();
            App.Self.EmitSignal(nameof(App.MainPageChanged), pageToOpen);
        }

        private void HidePages()
        {
            foreach (Node node in GetChildren())
            {
                if (node is Page page)
                {
                    page.Hide();
                }
            }
        }

        private AtlasPage GetAtlasPage()
        {
            return GetNode<AtlasPage>("./AtlasPage");
        }

        private ConfigPage GetConfigPage()
        {
            return GetNode<ConfigPage>("./ConfigPage");
        }
    }
}
