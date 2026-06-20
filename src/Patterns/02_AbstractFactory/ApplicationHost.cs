using CrossPlatformUISimulator.Patterns.FactoryMethod;

namespace CrossPlatformUISimulator.Patterns.AbstractFactory;

public class ApplicationHost
{
    private readonly IThemeFactory _themeFactory;
    private readonly IWidgetFactory _widgetFactory;

    public ApplicationHost(IThemeFactory themeFactory, IWidgetFactory widgetFactory)
    {
        _themeFactory = themeFactory;
        _widgetFactory = widgetFactory;
    }

    public void BuildTestWindow()
    {
        var dialogRenderer = _themeFactory.CreateDialogRenderer();
        var button = _themeFactory.CreateButton();
        var checkBox = _themeFactory.CreateCheckBox();

        dialogRenderer.RenderDialog("Тестовое окно");
        button.Draw();
        checkBox.Draw();

        var extraWidget = _widgetFactory.CreateWidget(new WidgetConfig
        {
            Type = WidgetType.Slider,
            Label = "Дополнительный слайдер"
        });
        extraWidget.Draw();
    }

    public void ValidateThemeCompatibility(string expectedTheme, string actualTheme)
    {
        if (expectedTheme != actualTheme)
        {
            throw new InvalidOperationException(
                $"Несовместимость тем: ожидалась {expectedTheme}, получена {actualTheme}");
        }
    }
}
