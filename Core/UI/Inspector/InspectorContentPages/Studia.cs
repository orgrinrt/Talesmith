using Godot;
using GDMechanic.Wiring;
using GDMechanic.Wiring.Attributes;

public class Studia : Control
{
	public override void _Ready()
    {
        this.Wire();
    }   
}