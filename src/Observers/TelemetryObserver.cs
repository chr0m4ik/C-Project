using UIFramework.Interfaces;
using UIFramework.Models;
using UIFramework.Factories;

namespace UIFramework.Observers;

public class TelemetryObserver : IUIStateObserver
{
    public void OnStateChange(string componentId, UIStateChangeData data)
    {
        ApplicationTelemetrySingleton.Instance.LogOperation("StateChange", componentId);
        Console.WriteLine($"TelemetryObserver: {componentId} {data.OldValue} -> {data.NewValue}");
    }
}
