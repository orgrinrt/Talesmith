using Godot;
using System;
using Talesmith.Core.UI.Pages;

public class ConfigPage : Page, ITabSwitchableItem
{
    public ITabSwitchableItem NextItem => GetNextItem();
    public ITabSwitchableItem PrevItem => GetPrevItem();
    
    public override void _Ready()
    {
        _pageEnum = PageEnum.WorldConfig;
    }

    public ITabSwitchableItem GetNextItem()
    {
        throw new NotImplementedException();
    }

    public ITabSwitchableItem GetPrevItem()
    {
        throw new NotImplementedException();
    }
}
