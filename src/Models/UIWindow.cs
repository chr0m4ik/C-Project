using UIFramework.Interfaces;

namespace UIFramework.Models;

public class UIWindow : IOriginator
{
    public string Title { get; set; } = "";
    public int X { get; set; }
    public int Y { get; set; }

    private class WindowMemento : IMemento
    {
        public string Title { get; }
        public int X { get; }
        public int Y { get; }

        public WindowMemento(string title, int x, int y)
        {
            Title = title;
            X = x;
            Y = y;
        }
    }

    public IMemento CreateMemento()
    {
        return new WindowMemento(Title, X, Y);
    }

    public void Restore(IMemento memento)
    {
        if (memento is not WindowMemento windowMemento)
        {
            throw new InvalidOperationException("Несовместимый снапшот для UIWindow");
        }

        Title = windowMemento.Title;
        X = windowMemento.X;
        Y = windowMemento.Y;
    }
}
