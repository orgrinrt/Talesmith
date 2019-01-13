using Godot;
using System;

public class AppPrefs : Resource
{
    [Export] public string Name = "test";
	[Export] public string Desc = "desc";
	[Export] public int Age = 666;
}
