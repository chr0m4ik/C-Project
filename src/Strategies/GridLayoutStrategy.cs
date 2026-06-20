using UIFramework.Interfaces;
using UIFramework.Models;

namespace UIFramework.Strategies;

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
