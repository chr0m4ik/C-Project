namespace CrossPlatformUISimulator.Patterns.Memento;

public class UIMementoManager
{
    private readonly List<(string Label, IMemento Snapshot, DateTime CreatedAt)> _checkpoints = new();
    private readonly int _maxCheckpoints;
    private readonly object _lock = new();

    public UIMementoManager(int maxCheckpoints = 20)
    {
        _maxCheckpoints = maxCheckpoints;
    }

    public void SaveCheckpoint(string label, IMemento snapshot)
    {
        lock (_lock)
        {
            _checkpoints.Add((label, snapshot, DateTime.Now));

            if (_checkpoints.Count > _maxCheckpoints)
            {
                _checkpoints.RemoveAt(0);
            }
        }
    }

    public IMemento? GetCheckpoint(string label)
    {
        lock (_lock)
        {
            for (int i = _checkpoints.Count - 1; i >= 0; i--)
            {
                if (_checkpoints[i].Label == label)
                {
                    return _checkpoints[i].Snapshot;
                }
            }

            return null;
        }
    }

    public void ClearHistory()
    {
        lock (_lock)
        {
            _checkpoints.Clear();
        }
    }

    public int Count
    {
        get
        {
            lock (_lock)
            {
                return _checkpoints.Count;
            }
        }
    }
}
