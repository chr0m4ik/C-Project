using UIFramework.Interfaces;
using UIFramework.Models;

namespace UIFramework.Commands;

public class MoveComponentCommand : IUICommand
{
    private readonly MovableComponent _target;
    private readonly int _newX;
    private readonly int _newY;
    private int _oldX;
    private int _oldY;

    public string Description => $"Перемещение {_target.Id} в ({_newX},{_newY})";

    public MoveComponentCommand(MovableComponent target, int newX, int newY)
    {
        _target = target;
        _newX = newX;
        _newY = newY;
    }

    public void Execute()
    {
        if (_target.IsLocked)
        {
            throw new InvalidOperationException($"Компонент {_target.Id} заблокирован, перемещение запрещено");
        }

        _oldX = _target.X;
        _oldY = _target.Y;
        _target.X = _newX;
        _target.Y = _newY;
    }

    public void Undo()
    {
        _target.X = _oldX;
        _target.Y = _oldY;
    }
}
