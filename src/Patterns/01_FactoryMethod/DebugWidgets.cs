namespace CrossPlatformUISimulator.Patterns.FactoryMethod;

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
