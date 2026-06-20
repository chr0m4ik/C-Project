namespace CrossPlatformUISimulator.Patterns.Command;

public class ThemedComponent
{
    public string Id { get; }
    public string StyleKey { get; set; }

    public ThemedComponent(string id, string styleKey)
    {
        Id = id;
        StyleKey = styleKey;
    }
}

public class ApplyThemeCommand : IUICommand
{
    private readonly ThemedComponent _target;
    private readonly string _newStyleKey;
    private string _oldStyleKey = "";

    public string Description => $"Смена темы {_target.Id} на {_newStyleKey}";

    public ApplyThemeCommand(ThemedComponent target, string newStyleKey)
    {
        _target = target;
        _newStyleKey = newStyleKey;
    }

    public void Execute()
    {
        _oldStyleKey = _target.StyleKey;
        _target.StyleKey = _newStyleKey;
    }

    public void Undo()
    {
        _target.StyleKey = _oldStyleKey;
    }
}

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
