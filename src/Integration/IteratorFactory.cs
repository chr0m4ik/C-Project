using UIFramework.Interfaces;

namespace UIFramework.Integration;

public class IteratorFactory
{
    public IEnumerator<IUIComponent> CreateDfs(IUIComponent root)
    {
        return new DepthFirstIterator(root);
    }

    public IEnumerator<IUIComponent> CreateFiltered(IUIComponent root, Func<IUIComponent, bool> predicate)
    {
        return new FilteredIterator(new DepthFirstIterator(root), predicate);
    }
}
