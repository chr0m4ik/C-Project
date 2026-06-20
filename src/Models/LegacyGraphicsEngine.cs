namespace UIFramework.Models;

public class LegacyGraphicsEngine
{
    public void DrawNativeButton(int x, int y, int width, int height, string legacyLabel)
    {
        Console.WriteLine($"[Legacy] Кнопка '{legacyLabel}' в точке ({x},{y}) размером {width}x{height}");
    }

    public void RenderTextRaster(string fontName, int size, int r, int g, int b, int x, int y, string text)
    {
        Console.WriteLine($"[Legacy] Текст '{text}' шрифтом {fontName} размер {size} цвет ({r},{g},{b}) в ({x},{y})");
    }

    public void ShowModalWindow(string title, bool blockInput)
    {
        Console.WriteLine($"[Legacy] Модальное окно '{title}', блокировка ввода: {blockInput}");
    }
}
