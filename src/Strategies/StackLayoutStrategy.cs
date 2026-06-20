using UIFramework.Interfaces;
using UIFramework.Models;

namespace UIFramework.Strategies;

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
