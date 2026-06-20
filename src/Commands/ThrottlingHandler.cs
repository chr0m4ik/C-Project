using UIFramework.Models;

namespace UIFramework.Commands;

public class ThrottlingHandler : BaseUIEventHandler
{
    private readonly int _maxEventsPerWindow;
    private readonly TimeSpan _window;
    private readonly Dictionary<string, List<DateTime>> _history = new();
    private readonly object _lock = new();

    public ThrottlingHandler(int maxEventsPerWindow, TimeSpan window)
    {
        _maxEventsPerWindow = maxEventsPerWindow;
        _window = window;
    }

    protected override bool TryHandle(UIEvent uiEvent)
    {
        lock (_lock)
        {
            if (!_history.ContainsKey(uiEvent.TargetComponentId))
            {
                _history[uiEvent.TargetComponentId] = new List<DateTime>();
            }

            var timestamps = _history[uiEvent.TargetComponentId];
            timestamps.RemoveAll(t => uiEvent.Timestamp - t > _window);

            if (timestamps.Count >= _maxEventsPerWindow)
            {
                Console.WriteLine($"ThrottlingHandler: событие для {uiEvent.TargetComponentId} отклонено (слишком часто)");
                return true;
            }

            timestamps.Add(uiEvent.Timestamp);
            return false;
        }
    }
}
