using CrossPlatformUISimulator.Patterns.Composite;
using CrossPlatformUISimulator.Patterns.Bridge;
using Xunit;

namespace CrossPlatformUISimulator.Tests;

public class CompositeAndBridgeTests
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
}
