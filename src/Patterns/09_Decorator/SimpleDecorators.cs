using CrossPlatformUISimulator.Patterns.Composite;
using CrossPlatformUISimulator.Patterns.Singleton;

namespace CrossPlatformUISimulator.Patterns.Decorator;

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

public class CachedRenderDecorator : UIComponentDecorator
{
    private bool _hasCache;

    public CachedRenderDecorator(IUIComponent component) : base(component)
    {
    }

    public override void Render()
    {
        if (_hasCache)
        {
            Console.WriteLine($"Используем кэш отрисовки для {Id}");
            return;
        }

        Component.Render();
        _hasCache = true;
    }

    public void InvalidateCache()
    {
        _hasCache = false;
    }
}
