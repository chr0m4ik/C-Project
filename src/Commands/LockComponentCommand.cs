using UIFramework.Interfaces;
using UIFramework.Models;

namespace UIFramework.Commands;

public class LockComponentCommand : IUICommand
{
    private readonly MovableComponent _target;
    private bool _wasLocked;

    public string Description => $"Блокировка {_target.Id}";

    public LockComponentCommand(MovableComponent target)
    {
        _target = target;
    }

    public void Execute()
    {
        _wasLocked = _target.IsLocked;
        _target.IsLocked = true;
    }

    public void Undo()
    {
        _target.IsLocked = _wasLocked;
    }
}
