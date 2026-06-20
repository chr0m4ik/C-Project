namespace UIFramework.Models;

public record LayoutContext(int Padding, int Spacing, int AvailableWidth);

public record LayoutResult(string ComponentId, int X, int Y);
