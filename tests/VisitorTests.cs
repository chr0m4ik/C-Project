using CrossPlatformUISimulator.Patterns.Visitor;
using Xunit;

namespace CrossPlatformUISimulator.Tests;

public class VisitorTests
{
    [Fact]
    public void MetricsCollectorVisitor_CountsButtons()
    {
        var panel = new VisitablePanel("panel1");
        panel.Children.Add(new VisitableButton("btn1"));
        panel.Children.Add(new VisitableButton("btn2"));

        var visitor = new MetricsCollectorVisitor();
        panel.Accept(visitor);

        Assert.Equal(2, visitor.Report.ButtonCount);
    }

    [Fact]
    public void DependencyValidatorVisitor_DetectsDuplicateId()
    {
        var panel = new VisitablePanel("panel1");
        panel.Children.Add(new VisitableButton("sameId"));
        panel.Children.Add(new VisitableButton("sameId"));

        var visitor = new DependencyValidatorVisitor();
        panel.Accept(visitor);

        Assert.Single(visitor.Violations);
    }

    [Fact]
    public void AccessibilityTreeVisitor_BuildsNodesForAllChildren()
    {
        var panel = new VisitablePanel("panel1");
        panel.Children.Add(new VisitableButton("btn1"));
        panel.Children.Add(new VisitableDialog("dialog1"));

        var visitor = new AccessibilityTreeVisitor();
        panel.Accept(visitor);

        Assert.Equal(3, visitor.AccessibleNodes.Count);
    }
}
