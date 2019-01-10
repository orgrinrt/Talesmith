using Godot;
using Taleteller.Core.Constants;

namespace Taleteller.Core.UI.Pages
{
    public class Page : Control
    {
        public override void _Ready()
        {
            base._Ready();
            
            AddToGroup(Groups.Pages);
        }
    }
}