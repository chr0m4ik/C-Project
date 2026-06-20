using UIFramework.Interfaces;

namespace UIFramework.Commands;

public class MacroCommand : IUICommand
{
    private readonly List<IUICommand> _commands;
    private readonly List<IUICommand> _executed = new();

    public string Description => "Макрокоманда из нескольких действий";

    public MacroCommand(List<IUICommand> commands)
    {
        _commands = commands;
    }

    public void Execute()
    {
        _executed.Clear();
        try
        {
            foreach (var command in _commands)
            {
                command.Execute();
                _executed.Add(command);
            }
        }
        catch
        {
            for (int i = _executed.Count - 1; i >= 0; i--)
            {
                _executed[i].Undo();
            }

            throw;
        }
    }

    public void Undo()
    {
        for (int i = _executed.Count - 1; i >= 0; i--)
        {
            _executed[i].Undo();
        }
    }
}
