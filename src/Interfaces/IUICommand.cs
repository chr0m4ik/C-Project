namespace UIFramework.Interfaces;

public interface IUICommand
{
    void Execute();
    void Undo();
    string Description { get; }
}
