namespace UIFramework.Models;

public record UIEvent(string EventType, string TargetComponentId, string? Payload, DateTime Timestamp);
