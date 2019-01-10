using Godot;
using System;
using Taleteller.Core.Viewports.Cameras;

public class AtlasPage : Control
{
    public override void _Ready()
    {
        
    }

    public FreeCamera GetFreeCamera()
    {
        return GetNode<FreeCamera>("../../Atlas2D/FreeCam");
    }
}
