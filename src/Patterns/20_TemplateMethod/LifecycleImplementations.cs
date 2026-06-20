namespace CrossPlatformUISimulator.Patterns.TemplateMethod;

public class StandardComponentLifecycle : UIComponentLifecycleBase
{
    protected override void Initialize(UIContext context)
    {
        Console.WriteLine($"{context.ComponentId}: инициализация");
    }

    protected override bool Validate(UIContext context)
    {
        return !string.IsNullOrWhiteSpace(context.ComponentId);
    }

    protected override void Render(UIContext context)
    {
        Console.WriteLine($"{context.ComponentId}: отрисовка стандартного компонента");
    }
}

public class ComplexContainerLifecycle : UIComponentLifecycleBase
{
    private readonly List<string> _children;

    public ComplexContainerLifecycle(List<string> children)
    {
        _children = children;
    }

    protected override void Initialize(UIContext context)
    {
        Console.WriteLine($"{context.ComponentId}: инициализация контейнера");
    }

    protected override void LoadResources(UIContext context)
    {
        Console.WriteLine($"{context.ComponentId}: загрузка {_children.Count} детей");
    }

    protected override bool Validate(UIContext context)
    {
        return _children.Count > 0;
    }

    protected override void Render(UIContext context)
    {
        Console.WriteLine($"{context.ComponentId}: отрисовка контейнера через стратегию раскладки");
    }
}
