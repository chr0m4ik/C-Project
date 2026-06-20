namespace UIFramework.Interfaces;

public interface IUIComponent
{
    string Id { get; }
    void Render();
    IUIComponent? FindById(string id);
}
