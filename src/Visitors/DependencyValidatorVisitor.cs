using UIFramework.Interfaces;
using UIFramework.Models;

namespace UIFramework.Visitors;

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
