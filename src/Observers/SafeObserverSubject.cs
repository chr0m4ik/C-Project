using UIFramework.Interfaces;
using UIFramework.Models;

namespace UIFramework.Observers;

public class SafeObserverSubject : IUIStateSubject
{
    private readonly List<IUIStateObserver> _observers = new();
    private readonly object _lock = new();

    public void Attach(IUIStateObserver observer)
    {
        lock (_lock)
        {
            _observers.Add(observer);
        }
    }

    public void Detach(IUIStateObserver observer)
    {
        lock (_lock)
        {
            _observers.Remove(observer);
        }
    }

    public void Notify(string componentId, UIStateChangeData data)
    {
        List<IUIStateObserver> snapshot;

        lock (_lock)
        {
            snapshot = new List<IUIStateObserver>(_observers);
        }

        foreach (var observer in snapshot)
        {
            observer.OnStateChange(componentId, data);
        }
    }
}
