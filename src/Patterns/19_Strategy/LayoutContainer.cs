namespace CrossPlatformUISimulator.Patterns.Strategy;

public class LayoutContainer
{
    private ILayoutStrategy _layoutStrategy;
    private readonly List<string> _componentIds;
    private readonly object _lock = new();

    public LayoutContainer(ILayoutStrategy layoutStrategy, List<string> componentIds)
    {
        _layoutStrategy = layoutStrategy;
        _componentIds = componentIds;
    }

    public void SetLayoutStrategy(ILayoutStrategy newStrategy)
    {
        lock (_lock)
        {
            _layoutStrategy = newStrategy;
        }
    }

    public List<LayoutResult> ApplyLayout(LayoutContext context)
    {
        lock (_lock)
        {
            return _layoutStrategy.CalculateBounds(_componentIds, context);
        }
    }
}
