namespace CrossPlatformUISimulator.Patterns.Strategy;

public class StackLayoutStrategy : ILayoutStrategy
{
    public List<LayoutResult> CalculateBounds(List<string> componentIds, LayoutContext context)
    {
        var results = new List<LayoutResult>();
        int currentY = context.Padding;

        foreach (var id in componentIds)
        {
            results.Add(new LayoutResult(id, context.Padding, currentY));
            currentY += context.Spacing + 30;
        }

        return results;
    }
}

public class GridLayoutStrategy : ILayoutStrategy
{
    private readonly int _columns;

    public GridLayoutStrategy(int columns)
    {
        _columns = columns;
    }

    public List<LayoutResult> CalculateBounds(List<string> componentIds, LayoutContext context)
    {
        var results = new List<LayoutResult>();
        int cellWidth = context.AvailableWidth / _columns;

        for (int i = 0; i < componentIds.Count; i++)
        {
            int column = i % _columns;
            int row = i / _columns;

            int x = context.Padding + column * cellWidth;
            int y = context.Padding + row * (30 + context.Spacing);

            results.Add(new LayoutResult(componentIds[i], x, y));
        }

        return results;
    }
}

public class FreeFormLayoutStrategy : ILayoutStrategy
{
    private readonly Dictionary<string, (int X, int Y)> _positions;

    public FreeFormLayoutStrategy(Dictionary<string, (int X, int Y)> positions)
    {
        _positions = positions;
    }

    public List<LayoutResult> CalculateBounds(List<string> componentIds, LayoutContext context)
    {
        var results = new List<LayoutResult>();

        foreach (var id in componentIds)
        {
            if (_positions.ContainsKey(id))
            {
                var pos = _positions[id];
                results.Add(new LayoutResult(id, pos.X, pos.Y));
            }
            else
            {
                results.Add(new LayoutResult(id, 0, 0));
            }
        }

        return results;
    }
}
