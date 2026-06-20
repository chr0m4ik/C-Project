namespace CrossPlatformUISimulator.Patterns.FactoryMethod;

public class StandardWidgetFactory : IWidgetFactory
{
    private readonly Dictionary<WidgetType, Func<string, IWidget>> _creators;

    public StandardWidgetFactory()
    {
        _creators = new Dictionary<WidgetType, Func<string, IWidget>>
        {
            [WidgetType.Button] = name => new ButtonWidget(name),
            [WidgetType.TextBox] = name => new TextBoxWidget(name),
            [WidgetType.Panel] = name => new PanelWidget(name),
            [WidgetType.Slider] = name => new SliderWidget(name)
        };
    }

    public IWidget CreateWidget(WidgetConfig config)
    {
        if (!_creators.ContainsKey(config.Type))
        {
            throw new InvalidOperationException($"Неизвестный тип виджета: {config.Type}");
        }

        return _creators[config.Type](config.Label);
    }
}
