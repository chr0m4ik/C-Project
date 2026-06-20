using UIFramework.Interfaces;

namespace UIFramework.Decorators;

public abstract class UIComponentDecorator : IUIComponent
{
    protected readonly IUIComponent Component;

    protected UIComponentDecorator(IUIComponent component)
    {
        Component = component;
    }

    public virtual string Id => Component.Id;

    public virtual void Render()
    {
        Component.Render();
    }

    public virtual IUIComponent? FindById(string id)
    {
        return Component.FindById(id);
    }
}
