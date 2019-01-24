using Godot;

namespace Talesmith.Core.Systems
{
	public class Preferences : Node
	{
		[Signal] public delegate void InspectorToggled(bool visible);
		[Signal] public delegate void BinderToggled(bool visible);
		[Signal] public delegate void DockToggled(bool visible);
		
		[Signal] public delegate void UseBigIconsToggled(bool use);
		[Signal] public delegate void AnimationSpeedChanged(float newValue);
	
		[Export()] public Resource ApplicationPreferences;
		[Export()] public Resource ViewPreferences;
		[Export()] public Resource AppearancePreferences;
		[Export()] public Resource ThemePreferences;
	
		public override void _Ready()
		{
	    	LoadTheme(ResourceLoader.Load<Resource>("res://Assets/Resources/Themes/Pagan/Pagan.tres"));
		}

		public void LoadTheme(Resource themeSet)
		{
			GD.Print(themeSet.Get("background_color"));
			VisualServer.SetDefaultClearColor((Color)themeSet.Get("background_color"));
		}
	}
}
