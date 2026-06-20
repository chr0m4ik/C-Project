using UIFramework.Interfaces;
using UIFramework.Models;

namespace UIFramework.Visitors;

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
