namespace CrossPlatformUISimulator.Patterns.Mediator;

public class EventDrivenMediator : IUIComponentMediator
{
    private readonly List<IMediatorComponent> _components = new();
    private readonly object _lock = new();

    public void Register(IMediatorComponent component)
    {
        lock (_lock)
        {
            _components.Add(component);
        }
    }

    public void Unregister(IMediatorComponent component)
    {
        lock (_lock)
        {
            _components.Remove(component);
        }
    }

    public void Notify(string senderId, MediatorEvent mediatorEvent)
    {
        List<IMediatorComponent> snapshot;

        lock (_lock)
        {
            snapshot = new List<IMediatorComponent>(_components);
        }

        foreach (var component in snapshot)
        {
            if (component.Id != senderId)
            {
                component.Receive(mediatorEvent);
            }
        }
    }
}
