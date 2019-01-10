using System.Collections.Generic;
using Godot;

namespace Taleteller.Core.UI.Inspector
{
    public class Inspector : Control
    {
        public override void _Ready()
        {
            
        }

        public void OpenPage(InspectorPageEnum page)
        {
            switch (page)
            {
                case InspectorPageEnum.Atlas:
                    HidePages();
                    GetAtlasPage().Show();
                    break;
            }
        }

        private void HidePages()
        {
            List<InspectorPage> Pages = new List<InspectorPage>();

            foreach (Node node in GetChildren())
            {
                if (node is InspectorPage page)
                {
                    page.Hide();
                }
            }
        }

        private InspectorPage GetAtlasPage()
        {
            return GetNode<InspectorPage>("./Atlas");
        }

        private Dragger GetDragger()
        {
            return GetNode<Dragger>("./Dragger");
        }
    }

    public enum InspectorPageEnum
    {
        Atlas
    }
}
