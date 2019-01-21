using Godot;
using GDMechanic.Wiring;
using GDMechanic.Wiring.Attributes;

public class Config : Control
{
	public override void _Ready()
    {
        this.Wire();
    }   
}