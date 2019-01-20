using GDMechanic.Wiring;
using Godot;

namespace Talesmith.Core.UI.Dock
{
    public class Dock : Control
    {
        public override void _Ready()
        {
            this.Wire();
        }   
    }
}