using Godot;
using GDMechanic.Wiring;
using GDMechanic.Wiring.Attributes;
using Talesmith.Core.UI.Inspector;

public class Home : InspectorContentPage
{
	public override void _Ready()
    {
        this.Wire();
    }   
}