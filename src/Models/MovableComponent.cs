namespace UIFramework.Models;

public class MovableComponent
{
    public string Id { get; }
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsLocked { get; set; }

    public MovableComponent(string id, int x, int y)
    {
        Id = id;
        X = x;
        Y = y;
    }
}
