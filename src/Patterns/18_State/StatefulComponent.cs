namespace CrossPlatformUISimulator.Patterns.State;

public class StatefulComponent
{
    public string Id { get; }
    private IComponentState _currentState;
    private readonly object _lock = new();

    public StatefulComponent(string id)
    {
        Id = id;
        _currentState = new NormalState();
    }

    public string CurrentStateName => _currentState.StateName;

    public void TransitionTo(IComponentState newState)
    {
        lock (_lock)
        {
            _currentState.Exit(this);
            _currentState = newState;
            _currentState.Enter(this);
        }
    }

    public void Click()
    {
        _currentState.HandleClick(this);
    }
}
