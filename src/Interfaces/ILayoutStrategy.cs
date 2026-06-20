using UIFramework.Models;

namespace UIFramework.Interfaces;

public interface ILayoutStrategy
{
    List<LayoutResult> CalculateBounds(List<string> componentIds, LayoutContext context);
}
