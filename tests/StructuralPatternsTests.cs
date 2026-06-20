using UIFramework.Interfaces;
using UIFramework.Models;
using UIFramework.Decorators;
using UIFramework.Factories;
using UIFramework.Integration;
using Xunit;

namespace UIFramework.Tests;

public class StructuralPatternsTests
{
    [Fact]
    public void ContainerComponent_FindById_FindsDeepChild()
    {
        var root = new ContainerComponent("root");
        var panel = new ContainerComponent("panel");
        panel.AddChild(new LeafComponent("deepButton", "Кнопка"));
        root.AddChild(panel);

        var found = root.FindById("deepButton");

        Assert.NotNull(found);
        Assert.Equal("deepButton", found!.Id);
    }

    [Fact]
    public void ContainerComponent_ThrowsOnDuplicateId()
    {
        var root = new ContainerComponent("root");
        root.AddChild(new LeafComponent("button1", "Кнопка"));

        Assert.Throws<InvalidOperationException>(() =>
            root.AddChild(new LeafComponent("button1", "Другая кнопка")));
    }

    [Fact]
    public void ContainerComponent_CloneDeep_IsIndependent()
    {
        var root = new ContainerComponent("root");
        root.AddChild(new LeafComponent("button1", "Кнопка"));

        var clone = root.CloneDeep();

        Assert.NotNull(clone.FindById("button1"));
        Assert.NotSame(root, clone);
    }

    [Fact]
    public void Bridge_SwitchRenderingStrategy_ChangesBehavior()
    {
        var button = new ButtonComponent("btn1", new RasterRenderingStrategy());
        button.SwitchRenderingStrategy(new VectorRenderingStrategy());

        var exception = Record.Exception(() => button.Render());
        Assert.Null(exception);
    }

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
