using Godot;
using System;

public class Preferences : Node
{
	[Signal] public delegate void InspectorToggled(bool visible);
	[Signal] public delegate void AnimationSpeedChanged(float newValue);
	
	[Export()] public Resource ViewPreferences;
	[Export()] public Resource AppearancePreferences;
	
    public override void _Ready()
    {
	    
    }
}
