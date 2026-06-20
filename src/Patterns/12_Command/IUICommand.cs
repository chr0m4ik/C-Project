namespace CrossPlatformUISimulator.Patterns.Command;

public interface IUICommand
{
    void Execute();
    void Undo();
    string Description { get; }
}
