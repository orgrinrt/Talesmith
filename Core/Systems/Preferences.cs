using Godot;
using System;

public class Preferences : Node
{
	[Signal] public delegate void InspectorToggled(bool visible);
	
	[Export()] public Resource ViewPreferences;
	[Export()] public Resource AppPreferences;
	
    public override void _Ready()
    {
	    
    }
}
