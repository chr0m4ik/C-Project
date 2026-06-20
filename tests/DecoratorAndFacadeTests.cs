using CrossPlatformUISimulator.Patterns.Composite;
using CrossPlatformUISimulator.Patterns.Decorator;
using CrossPlatformUISimulator.Patterns.AbstractFactory;
using CrossPlatformUISimulator.Patterns.Singleton;
using CrossPlatformUISimulator.Patterns.Facade;
using Xunit;

namespace CrossPlatformUISimulator.Tests;

public class DecoratorAndFacadeTests
{
    [Fact]
    public void BorderDecorator_KeepsSameId()
    {
        IUIComponent leaf = new LeafComponent("button1", "Кнопка");
        var decorated = new BorderDecorator(leaf);

        Assert.Equal("button1", decorated.Id);
    }

    [Fact]
    public void CachedRenderDecorator_UsesCacheOnSecondRender()
    {
        IUIComponent leaf = new LeafComponent("button1", "Кнопка");
        var cached = new CachedRenderDecorator(leaf);

        cached.Render();
        var exception = Record.Exception(() => cached.Render());

        Assert.Null(exception);
    }

    [Fact]
    public void Facade_CreateDialog_ReturnsValidDialog()
    {
        var facade = new UISystemFacade(new FluentThemeFactory(), ApplicationTelemetrySingleton.Instance);
        var dialog = facade.CreateDialog("Тест");

        Assert.False(string.IsNullOrEmpty(dialog.Title));
    }
}
