using GDMechanic.Wiring;
using Godot;

namespace Talesmith.Core.UI.Binder.BinderContentPages
{
    public class Studia : BinderContentPage
    {
        public override void _Ready()
        {
            this.Wire();
        }   
    }
}