using UIFramework.Interfaces;

namespace UIFramework.Commands;

public class CommandManager
{
    private readonly Stack<IUICommand> _history = new();
    private readonly Stack<IUICommand> _redo = new();
    private readonly int _maxHistorySize;
    private readonly object _lock = new();

    public CommandManager(int maxHistorySize = 100)
    {
        _maxHistorySize = maxHistorySize;
    }

    public void Execute(IUICommand command)
    {
        lock (_lock)
        {
            command.Execute();
            _history.Push(command);
            _redo.Clear();

            if (_history.Count > _maxHistorySize)
            {
                var temp = new Stack<IUICommand>();
                for (int i = 0; i < _maxHistorySize; i++)
                {
                    temp.Push(_history.Pop());
                }

                _history.Clear();
                while (temp.Count > 0)
                {
                    _history.Push(temp.Pop());
                }
            }
        }
    }

    public void Undo()
    {
        lock (_lock)
        {
            if (_history.Count == 0)
            {
                return;
            }

            var command = _history.Pop();
            command.Undo();
            _redo.Push(command);
        }
    }

    public void Redo()
    {
        lock (_lock)
        {
            if (_redo.Count == 0)
            {
                return;
            }

            var command = _redo.Pop();
            command.Execute();
            _history.Push(command);
        }
    }

    public void ClearHistory()
    {
        lock (_lock)
        {
            _history.Clear();
            _redo.Clear();
        }
    }

    public int HistoryCount => _history.Count;
}
