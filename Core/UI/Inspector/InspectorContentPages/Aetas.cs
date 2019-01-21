using Godot;
using GDMechanic.Wiring;
using GDMechanic.Wiring.Attributes;

public class Aetas : Control
{
	public override void _Ready()
    {
        this.Wire();
    }   
}