using Godot;

namespace Talesmith.Core.Systems
{
	public class Preferences : Node
	{
		[Signal] public delegate void InspectorToggled(bool visible);
		[Signal] public delegate void UseBigIconsToggled(bool use);
		[Signal] public delegate void AnimationSpeedChanged(float newValue);
	
		[Export()] public Resource ApplicationPreferences;
		[Export()] public Resource ViewPreferences;
		[Export()] public Resource AppearancePreferences;
	
		public override void _Ready()
		{
	    
		}
	}
}
