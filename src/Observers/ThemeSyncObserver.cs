using UIFramework.Interfaces;
using UIFramework.Models;

namespace UIFramework.Observers;

public class ThemeSyncObserver : IUIStateObserver
{
    public void OnStateChange(string componentId, UIStateChangeData data)
    {
        Console.WriteLine($"ThemeSyncObserver: обновляем кэш рендера для {componentId}");
    }
}
