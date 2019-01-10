using Godot;
using System;
using Taleteller.Core.Viewports.Cameras;

public class AtlasPage : Node
{
    public override void _Ready()
    {
        
    }

    public FreeCamera GetFreeCamera()
    {
        return GetNode<FreeCamera>("./2D/FreeCam");
    }
}
