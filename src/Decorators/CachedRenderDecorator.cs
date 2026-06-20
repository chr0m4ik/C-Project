using UIFramework.Interfaces;

namespace UIFramework.Decorators;

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
