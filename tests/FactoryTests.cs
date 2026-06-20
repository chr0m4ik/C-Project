using CrossPlatformUISimulator.Patterns.FactoryMethod;
using CrossPlatformUISimulator.Patterns.AbstractFactory;
using Xunit;

namespace CrossPlatformUISimulator.Tests;

public class FactoryTests
{
    [Fact]
    public void StandardWidgetFactory_CreatesButton()
    {
        IWidgetFactory factory = new StandardWidgetFactory();
        var widget = factory.CreateWidget(new WidgetConfig { Type = WidgetType.Button, Label = "Тест" });

        Assert.IsType<ButtonWidget>(widget);
    }

    [Fact]
    public void DebugWidgetFactory_CreatesDebugButton()
    {
        IWidgetFactory factory = new DebugWidgetFactory();
        var widget = factory.CreateWidget(new WidgetConfig { Type = WidgetType.Button, Label = "Тест" });

        Assert.IsType<DebugButtonWidget>(widget);
    }

    [Fact]
    public void FluentThemeFactory_CreatesFluentComponents()
    {
        IThemeFactory themeFactory = new FluentThemeFactory();
        var button = themeFactory.CreateButton();

        Assert.Equal("Fluent", button.ThemeName);
    }

    [Fact]
    public void CupertinoThemeFactory_CreatesCupertinoComponents()
    {
        IThemeFactory themeFactory = new CupertinoThemeFactory();
        var checkBox = themeFactory.CreateCheckBox();

        Assert.Equal("Cupertino", checkBox.ThemeName);
    }

    [Fact]
    public void ApplicationHost_ThrowsOnThemeMismatch()
    {
        var host = new ApplicationHost(new FluentThemeFactory(), new StandardWidgetFactory());

        Assert.Throws<InvalidOperationException>(() =>
            host.ValidateThemeCompatibility("Fluent", "Cupertino"));
    }
}
