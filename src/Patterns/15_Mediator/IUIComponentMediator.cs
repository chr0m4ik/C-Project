namespace CrossPlatformUISimulator.Patterns.Mediator;

public record MediatorEvent(string SenderId, string EventType, string? Payload);

public interface IMediatorComponent
{
    string Id { get; }
    void Receive(MediatorEvent mediatorEvent);
}

public interface IUIComponentMediator
{
    void Register(IMediatorComponent component);
    void Unregister(IMediatorComponent component);
    void Notify(string senderId, MediatorEvent mediatorEvent);
}
