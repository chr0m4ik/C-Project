using UIFramework.Interfaces;

namespace UIFramework.Decorators;

public class BorderDecorator : UIComponentDecorator
{
    public BorderDecorator(IUIComponent component) : base(component)
    {
    }

    public override void Render()
    {
        Console.WriteLine($"Рамка вокруг {Id} начало");
        Component.Render();
        Console.WriteLine($"Рамка вокруг {Id} конец");
    }
}
