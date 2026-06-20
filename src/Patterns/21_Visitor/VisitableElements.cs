namespace CrossPlatformUISimulator.Patterns.Visitor;

public class VisitableButton : IVisitableComponent
{
    public string Id { get; }

    public VisitableButton(string id)
    {
        Id = id;
    }

    public void Accept(IUIComponentVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class VisitablePanel : IVisitableComponent
{
    public string Id { get; }
    public List<IVisitableComponent> Children { get; } = new();

    public VisitablePanel(string id)
    {
        Id = id;
    }

    public void Accept(IUIComponentVisitor visitor)
    {
        visitor.Visit(this);

        foreach (var child in Children)
        {
            child.Accept(visitor);
        }
    }
}

public class VisitableDialog : IVisitableComponent
{
    public string Id { get; }

    public VisitableDialog(string id)
    {
        Id = id;
    }

    public void Accept(IUIComponentVisitor visitor)
    {
        visitor.Visit(this);
    }
}
