using UIFramework.Interfaces;
using UIFramework.Factories;

namespace UIFramework.Decorators;

public class RenderLogDecorator : UIComponentDecorator
{
    public RenderLogDecorator(IUIComponent component) : base(component)
    {
    }

    public override void Render()
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        Component.Render();
        watch.Stop();

        ApplicationTelemetrySingleton.Instance.LogOperation("Render", Id);
        Console.WriteLine($"Render({Id}) занял {watch.ElapsedMilliseconds} мс");
    }
}
