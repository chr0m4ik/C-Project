namespace CrossPlatformUISimulator.Patterns.FactoryMethod;

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
