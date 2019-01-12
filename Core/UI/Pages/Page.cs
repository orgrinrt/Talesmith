using Godot;
using Talesmith.Core.Constants;

namespace Talesmith.Core.UI.Pages
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