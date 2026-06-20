using UIFramework.Interfaces;

namespace UIFramework.Models;

public abstract class UIComponentBase
{
    protected IRenderingStrategy RenderingStrategy;
    public string Name { get; }

    protected UIComponentBase(string name, IRenderingStrategy renderingStrategy)
    {
        Name = name;
        RenderingStrategy = renderingStrategy;
    }

    public void Render()
    {
        RenderingStrategy.DrawBackground(Name);
        RenderingStrategy.DrawBorder(Name);
        RenderingStrategy.DrawText(Name);
    }

    public void SwitchRenderingStrategy(IRenderingStrategy newStrategy)
    {
        RenderingStrategy = newStrategy;
    }
}

public class ButtonComponent : UIComponentBase
{
    public ButtonComponent(string name, IRenderingStrategy renderingStrategy)
        : base(name, renderingStrategy)
    {
    }
}

public class PanelComponent : UIComponentBase
{
    public PanelComponent(string name, IRenderingStrategy renderingStrategy)
        : base(name, renderingStrategy)
    {
    }
}
