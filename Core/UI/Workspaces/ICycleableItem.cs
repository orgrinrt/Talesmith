namespace Talesmith.Core.UI.Workspaces
{
    public interface ICycleableItem
    {
        ICycleableItem NextItem { get; }
        ICycleableItem PrevItem { get; }
        
        ICycleableItem GetNextItem();
        ICycleableItem GetPrevItem();
    }
}