namespace Talesmith.Core.UI.Pages
{
    public interface ITabSwitchableItem
    {
        ITabSwitchableItem NextItem { get; }
        ITabSwitchableItem PrevItem { get; }
        
        ITabSwitchableItem GetNextItem();
        ITabSwitchableItem GetPrevItem();
    }
}