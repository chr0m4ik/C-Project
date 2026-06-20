namespace CrossPlatformUISimulator.Patterns.Composite;

public class LeafComponent : IUIComponent
{
    public string Id { get; }
    private readonly string _label;

    public LeafComponent(string id, string label)
    {
        Id = id;
        _label = label;
    }

    public void Render()
    {
        Console.WriteLine($"Лист [{Id}]: {_label}");
    }

    public IUIComponent? FindById(string id)
    {
        return Id == id ? this : null;
    }
}
