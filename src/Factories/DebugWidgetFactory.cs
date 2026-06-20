using UIFramework.Interfaces;
using UIFramework.Models;

namespace UIFramework.Factories;

public class DebugWidgetFactory : IWidgetFactory
{
    private readonly Dictionary<WidgetType, Func<string, IWidget>> _creators;

    public DebugWidgetFactory()
    {
        _creators = new Dictionary<WidgetType, Func<string, IWidget>>
        {
            [WidgetType.Button] = name => new DebugButtonWidget(name),
            [WidgetType.TextBox] = name => new DebugTextBoxWidget(name),
            [WidgetType.Panel] = name => new DebugPanelWidget(name),
            [WidgetType.Slider] = name => new DebugSliderWidget(name)
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
