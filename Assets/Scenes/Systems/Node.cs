using Godot;
using GDMechanic.Wiring;
using GDMechanic.Wiring.Attributes;

public class Node : Godot.Node
{
	public override void _Ready()
    {
        this.Wire();
    }   
}