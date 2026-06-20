namespace UIFramework.Models;

public record MediatorEvent(string SenderId, string EventType, string? Payload);
