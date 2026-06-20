namespace CrossPlatformUISimulator.Patterns.TemplateMethod;

public class UIContext
{
    public string ComponentId { get; init; } = "";
}

public abstract class UIComponentLifecycleBase
{
    public sealed void ExecuteLifecycle(UIContext context)
    {
        Initialize(context);
        LoadResources(context);
        ApplyTheme(context);

        if (!Validate(context))
        {
            OnValidationFailed(context);
            return;
        }

        PreRender(context);
        Render(context);
        PostRender(context);
        Cleanup(context);
    }

    protected abstract void Initialize(UIContext context);

    protected virtual void LoadResources(UIContext context)
    {
    }

    protected virtual void ApplyTheme(UIContext context)
    {
    }

    protected abstract bool Validate(UIContext context);

    protected virtual void OnValidationFailed(UIContext context)
    {
        Console.WriteLine($"{context.ComponentId}: валидация не пройдена");
    }

    protected virtual void PreRender(UIContext context)
    {
    }

    protected abstract void Render(UIContext context);

    protected virtual void PostRender(UIContext context)
    {
    }

    protected virtual void Cleanup(UIContext context)
    {
    }
}
