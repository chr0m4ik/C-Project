namespace CrossPlatformUISimulator.Patterns.Composite;

public interface IUIComponent
{
    string Id { get; }
    void Render();
    IUIComponent? FindById(string id);
}
