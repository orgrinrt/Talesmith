using Godot;
using Talesmith.Core.Constants;

namespace Talesmith.Core.UI.Pages
{
    public class Page : Control
    {
        internal PageEnum _pageEnum;
        
        public PageEnum PageEnum => _pageEnum;
        
        public override void _Ready()
        {
            base._Ready();
            
            AddToGroup(Groups.Pages);
        }
    }
}