using CrossPlatformUISimulator.Patterns.Composite;
using CrossPlatformUISimulator.Patterns.Iterator;
using CrossPlatformUISimulator.Patterns.Interpreter;
using Xunit;

namespace CrossPlatformUISimulator.Tests;

public class IteratorAndInterpreterTests
{
    [Fact]
    public void DepthFirstIterator_VisitsAllNodes()
    {
        var root = new ContainerComponent("root");
        root.AddChild(new LeafComponent("a", "A"));
        root.AddChild(new LeafComponent("b", "B"));

        var iterator = new DepthFirstIterator(root);
        int count = 0;
        while (iterator.MoveNext())
        {
            count++;
        }

        Assert.Equal(3, count);
    }

    [Fact]
    public void FilteredIterator_OnlyReturnsMatching()
    {
        var root = new ContainerComponent("root");
        root.AddChild(new LeafComponent("a", "A"));
        root.AddChild(new LeafComponent("b", "B"));

        var filtered = new FilteredIterator(new DepthFirstIterator(root), c => c.Id == "a");
        int count = 0;
        while (filtered.MoveNext())
        {
            count++;
        }

        Assert.Equal(1, count);
    }

    [Fact]
    public void DslParser_ParsesChainCorrectly()
    {
        var parser = new DslParser();
        var expression = parser.Parse("EXECUTE ApplyTheme('Fluent') -> Lock()");
        var context = new InterpreterContext();

        expression.Interpret(context);

        Assert.Equal(2, context.Log.Count);
    }

    [Fact]
    public void DslParser_ThrowsOnEmptyScript()
    {
        var parser = new DslParser();

        Assert.Throws<ParseException>(() => parser.Parse(""));
    }

    [Fact]
    public void DslParser_ThrowsOnUnknownAction()
    {
        var parser = new DslParser();

        Assert.Throws<ParseException>(() => parser.Parse("EXECUTE UnknownAction()"));
    }
}
