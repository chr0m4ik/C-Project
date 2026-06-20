namespace CrossPlatformUISimulator.Patterns.FactoryMethod;

public enum WidgetType
{
    Button,
    TextBox,
    Panel,
    Slider
}

public class WidgetConfig
{
    public required WidgetType Type { get; set; }
    public required string Label { get; set; }
}
