using UIFramework.Interfaces;

namespace UIFramework.Models;

public class ButtonWidget : IWidget
{
    public string Name { get; }

    public ButtonWidget(string name)
    {
        Name = name;
    }

    public void Draw()
    {
        Console.WriteLine($"Кнопка: {Name}");
    }
}

public class TextBoxWidget : IWidget
{
    public string Name { get; }

    public TextBoxWidget(string name)
    {
        Name = name;
    }

    public void Draw()
    {
        Console.WriteLine($"Текстовое поле: {Name}");
    }
}

public class PanelWidget : IWidget
{
    public string Name { get; }

    public PanelWidget(string name)
    {
        Name = name;
    }

    public void Draw()
    {
        Console.WriteLine($"Панель: {Name}");
    }
}

public class SliderWidget : IWidget
{
    public string Name { get; }

    public SliderWidget(string name)
    {
        Name = name;
    }

    public void Draw()
    {
        Console.WriteLine($"Слайдер: {Name}");
    }
}

public class DebugButtonWidget : IWidget
{
    public string Name { get; }

    public DebugButtonWidget(string name)
    {
        Name = name;
    }

    public void Draw()
    {
        Console.WriteLine($"[DEBUG] Кнопка с обводкой: {Name}");
    }
}

public class DebugTextBoxWidget : IWidget
{
    public string Name { get; }

    public DebugTextBoxWidget(string name)
    {
        Name = name;
    }

    public void Draw()
    {
        Console.WriteLine($"[DEBUG] Текстовое поле с обводкой: {Name}");
    }
}

public class DebugPanelWidget : IWidget
{
    public string Name { get; }

    public DebugPanelWidget(string name)
    {
        Name = name;
    }

    public void Draw()
    {
        Console.WriteLine($"[DEBUG] Панель с обводкой: {Name}");
    }
}

public class DebugSliderWidget : IWidget
{
    public string Name { get; }

    public DebugSliderWidget(string name)
    {
        Name = name;
    }

    public void Draw()
    {
        Console.WriteLine($"[DEBUG] Слайдер с обводкой: {Name}");
    }
}
