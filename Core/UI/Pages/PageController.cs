using Godot;
using Talesmith.Core.UI.Pages.Atlas;

namespace Talesmith.Core.UI.Pages
{
    public class PageController : Control
    {
        private Page _currPage;
        public Page CurrPage => _currPage;
        
        public override void _Ready()
        {
            MarginRight = -App.Self.GetInspector().RectSize.x;
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
            _currPage = pageToOpen;
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
