using UIFramework.Interfaces;

namespace UIFramework.Models;

public class RasterRenderingStrategy : IRenderingStrategy
{
    public void DrawBackground(string componentName)
    {
        Console.WriteLine($"[Растровая отрисовка] фон для {componentName}");
    }

    public void DrawBorder(string componentName)
    {
        Console.WriteLine($"[Растровая отрисовка] рамка для {componentName}");
    }

    public void DrawText(string text)
    {
        Console.WriteLine($"[Растровая отрисовка] текст: {text}");
    }
}

public class VectorRenderingStrategy : IRenderingStrategy
{
    public void DrawBackground(string componentName)
    {
        Console.WriteLine($"[Векторная отрисовка] фон для {componentName}");
    }

    public void DrawBorder(string componentName)
    {
        Console.WriteLine($"[Векторная отрисовка] рамка для {componentName}");
    }

    public void DrawText(string text)
    {
        Console.WriteLine($"[Векторная отрисовка] текст: {text}");
    }
}
