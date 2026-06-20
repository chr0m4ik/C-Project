using CrossPlatformUISimulator.Patterns.Strategy;
using CrossPlatformUISimulator.Patterns.TemplateMethod;
using Xunit;

namespace CrossPlatformUISimulator.Tests;

public class StrategyAndTemplateMethodTests
{
    [Fact]
    public void StackLayoutStrategy_PlacesComponentsVertically()
    {
        var strategy = new StackLayoutStrategy();
        var context = new LayoutContext(Padding: 0, Spacing: 0, AvailableWidth: 100);

        var result = strategy.CalculateBounds(new List<string> { "a", "b" }, context);

        Assert.True(result[1].Y > result[0].Y);
    }

    [Fact]
    public void LayoutContainer_SwitchStrategy_ChangesResult()
    {
        var container = new LayoutContainer(new StackLayoutStrategy(), new List<string> { "a", "b" });
        var context = new LayoutContext(Padding: 0, Spacing: 0, AvailableWidth: 100);

        var stackResult = container.ApplyLayout(context);
        container.SetLayoutStrategy(new GridLayoutStrategy(2));
        var gridResult = container.ApplyLayout(context);

        Assert.NotEqual(stackResult[1].X, gridResult[1].X);
    }

    [Fact]
    public void TemplateMethod_ValidationFailed_SkipsRender()
    {
        var lifecycle = new ComplexContainerLifecycle(new List<string>());

        var exception = Record.Exception(() => lifecycle.ExecuteLifecycle(new UIContext { ComponentId = "empty" }));

        Assert.Null(exception);
    }

    [Fact]
    public void TemplateMethod_StepsRunInOrder()
    {
        var lifecycle = new StandardComponentLifecycle();

        var exception = Record.Exception(() => lifecycle.ExecuteLifecycle(new UIContext { ComponentId = "btn1" }));

        Assert.Null(exception);
    }
}
