namespace Talesmith.Core.UI.Pages
{
    public interface ICycleableItem
    {
        ICycleableItem NextItem { get; }
        ICycleableItem PrevItem { get; }
        
        ICycleableItem GetNextItem();
        ICycleableItem GetPrevItem();
    }
}