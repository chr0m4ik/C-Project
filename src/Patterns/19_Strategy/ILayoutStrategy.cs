namespace CrossPlatformUISimulator.Patterns.Strategy;

public record LayoutContext(int Padding, int Spacing, int AvailableWidth);

public record LayoutResult(string ComponentId, int X, int Y);

public interface ILayoutStrategy
{
    List<LayoutResult> CalculateBounds(List<string> componentIds, LayoutContext context);
}
