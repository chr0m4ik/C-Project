using UIFramework.Interfaces;
using UIFramework.Models;

namespace UIFramework.Strategies;

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
