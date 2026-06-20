namespace CrossPlatformUISimulator.Patterns.Observer;

public record UIStateChangeData(string StateType, string OldValue, string NewValue, DateTime Timestamp);

public interface IUIStateObserver
{
    void OnStateChange(string componentId, UIStateChangeData data);
}

public interface IUIStateSubject
{
    void Attach(IUIStateObserver observer);
    void Detach(IUIStateObserver observer);
    void Notify(string componentId, UIStateChangeData data);
}
