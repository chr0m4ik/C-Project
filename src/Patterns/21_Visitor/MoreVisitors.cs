namespace CrossPlatformUISimulator.Patterns.Visitor;

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

public class ValidationViolation
{
    public required string ComponentId { get; set; }
    public required string Reason { get; set; }
}

public class DependencyValidatorVisitor : IUIComponentVisitor
{
    public List<ValidationViolation> Violations { get; } = new();
    private readonly HashSet<string> _seenIds = new();

    public void Visit(VisitableButton button)
    {
        CheckDuplicate(button.Id);
    }

    public void Visit(VisitablePanel panel)
    {
        CheckDuplicate(panel.Id);
    }

    public void Visit(VisitableDialog dialog)
    {
        CheckDuplicate(dialog.Id);
    }

    private void CheckDuplicate(string id)
    {
        if (!_seenIds.Add(id))
        {
            Violations.Add(new ValidationViolation
            {
                ComponentId = id,
                Reason = "Дублирование Id в дереве"
            });
        }
    }
}
