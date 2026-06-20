using UIFramework.Interfaces;
using UIFramework.Models;

namespace UIFramework.Visitors;

public class AccessibilityTreeVisitor : IUIComponentVisitor
{
    public List<string> AccessibleNodes { get; } = new();

    public void Visit(VisitableButton button)
    {
        AccessibleNodes.Add($"role=button id={button.Id}");
    }

    public void Visit(VisitablePanel panel)
    {
        AccessibleNodes.Add($"role=panel id={panel.Id}");
    }

    public void Visit(VisitableDialog dialog)
    {
        AccessibleNodes.Add($"role=dialog id={dialog.Id}");
    }
}
