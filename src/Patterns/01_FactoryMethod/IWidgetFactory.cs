namespace CrossPlatformUISimulator.Patterns.FactoryMethod;

public interface IWidgetFactory
{
    IWidget CreateWidget(WidgetConfig config);
}
