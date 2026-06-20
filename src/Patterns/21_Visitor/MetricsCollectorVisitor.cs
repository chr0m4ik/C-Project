namespace CrossPlatformUISimulator.Patterns.Visitor;

public class MetricsReport
{
    public int ButtonCount { get; set; }
    public int PanelCount { get; set; }
    public int DialogCount { get; set; }
}

public class MetricsCollectorVisitor : IUIComponentVisitor
{
    public MetricsReport Report { get; } = new();

    public void Visit(VisitableButton button)
    {
        Report.ButtonCount++;
    }

    public void Visit(VisitablePanel panel)
    {
        Report.PanelCount++;
    }

    public void Visit(VisitableDialog dialog)
    {
        Report.DialogCount++;
    }
}
