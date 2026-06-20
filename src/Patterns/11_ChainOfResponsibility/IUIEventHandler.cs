namespace CrossPlatformUISimulator.Patterns.ChainOfResponsibility;

public record UIEvent(string EventType, string TargetComponentId, string? Payload, DateTime Timestamp);

public interface IUIEventHandler
{
    IUIEventHandler SetNext(IUIEventHandler next);
    bool Handle(UIEvent uiEvent);
}
