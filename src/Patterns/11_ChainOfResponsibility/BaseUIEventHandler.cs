namespace CrossPlatformUISimulator.Patterns.ChainOfResponsibility;

public abstract class BaseUIEventHandler : IUIEventHandler
{
    private IUIEventHandler? _next;

    public IUIEventHandler SetNext(IUIEventHandler next)
    {
        _next = next;
        return this;
    }

    public bool Handle(UIEvent uiEvent)
    {
        if (TryHandle(uiEvent))
        {
            return true;
        }

        if (_next != null)
        {
            return _next.Handle(uiEvent);
        }

        return false;
    }

    protected abstract bool TryHandle(UIEvent uiEvent);
}
