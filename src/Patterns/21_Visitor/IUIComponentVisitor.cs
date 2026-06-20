namespace CrossPlatformUISimulator.Patterns.Visitor;

public interface IUIComponentVisitor
{
    void Visit(VisitableButton button);
    void Visit(VisitablePanel panel);
    void Visit(VisitableDialog dialog);
}

public interface IVisitableComponent
{
    void Accept(IUIComponentVisitor visitor);
}
