using CrossPlatformUISimulator.Patterns.Singleton;

namespace CrossPlatformUISimulator.Patterns.Observer;

public class TelemetryObserver : IUIStateObserver
{
    public void OnStateChange(string componentId, UIStateChangeData data)
    {
        ApplicationTelemetrySingleton.Instance.LogOperation("StateChange", componentId);
        Console.WriteLine($"TelemetryObserver: {componentId} {data.OldValue} -> {data.NewValue}");
    }
}

public class ThemeSyncObserver : IUIStateObserver
{
    public void OnStateChange(string componentId, UIStateChangeData data)
    {
        Console.WriteLine($"ThemeSyncObserver: обновляем кэш рендера для {componentId}");
    }
}
