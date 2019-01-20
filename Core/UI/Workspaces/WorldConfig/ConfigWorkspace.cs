using Godot;
using System;
using Talesmith.Core.UI.Pages;

public class ConfigWorkspace : Workspace, ICycleableItem
{
    public ICycleableItem NextItem => GetNextItem();
    public ICycleableItem PrevItem => GetPrevItem();
    
    public override void _Ready()
    {
        _workspaceEnum = WorkspaceEnum.WorldConfig;
    }

    public ICycleableItem GetNextItem()
    {
        throw new NotImplementedException();
    }

    public ICycleableItem GetPrevItem()
    {
        throw new NotImplementedException();
    }
}
