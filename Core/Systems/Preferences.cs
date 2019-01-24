using System.Linq;
using Godot;
using Godot.Collections;
using Talesmith.Core.Constants;
using Talesmith.Core.UI;

namespace Talesmith.Core.Systems
{
	public class Preferences : Node
	{
		[Signal] public delegate void InspectorToggled(bool visible);
		[Signal] public delegate void BinderToggled(bool visible);
		[Signal] public delegate void DockToggled(bool visible);
		
		[Signal] public delegate void UseBigIconsToggled(bool use);
		[Signal] public delegate void AnimationSpeedChanged(float newValue);
		[Signal] public delegate void ThemeSetChanged(Resource newThemeSet);
	
		[Export()] public Resource ApplicationPreferences;
		[Export()] public Resource ViewPreferences;
		[Export()] public Resource AppearancePreferences;

		public override void _Ready()
		{
	    	LoadTheme(GetCurrentThemeSet());

			Connect(nameof(InspectorToggled), this, nameof(SavePreferences));
			Connect(nameof(BinderToggled), this, nameof(SavePreferences));
			Connect(nameof(DockToggled), this, nameof(SavePreferences));

			Connect(nameof(UseBigIconsToggled), this, nameof(SavePreferences));
			Connect(nameof(AnimationSpeedChanged), this, nameof(SavePreferences));
			Connect(nameof(ThemeSetChanged), this, nameof(SavePreferences));
		}

		public void SavePreferences(bool visible)
		{
			GD.Print("SAVED");
			ResourceSaver.Save(ApplicationPreferences.ResourcePath, ApplicationPreferences);
			ResourceSaver.Save(ViewPreferences.ResourcePath, ViewPreferences);
			ResourceSaver.Save(AppearancePreferences.ResourcePath, AppearancePreferences);
		}
		
		public void SavePreferences(float value)
		{
			GD.Print("SAVED");
			ResourceSaver.Save(ApplicationPreferences.ResourcePath, ApplicationPreferences);
			ResourceSaver.Save(ViewPreferences.ResourcePath, ViewPreferences);
			ResourceSaver.Save(AppearancePreferences.ResourcePath, AppearancePreferences);
		}

		public void LoadTheme(Resource themeSet)
		{
			VisualServer.SetDefaultClearColor((Color)themeSet.Get("background_color"));

			Array themeables = GetTree().GetNodesInGroup(Groups.Themeable);
			foreach (Node node in themeables)
			{
				if (node is IThemeable themeable)
				{
					themeable.UpdateTheme(themeSet);
				}
			}
			
			EmitSignal(nameof(ThemeSetChanged), themeSet);
		}

		public Resource GetCurrentThemeSet()
		{
			return (Resource)AppearancePreferences.Get("current_theme_set");
		}
	}
}
