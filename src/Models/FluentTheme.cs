using UIFramework.Interfaces;

namespace UIFramework.Models;

public class FluentButton : IButton
{
    public string ThemeName => "Fluent";

    public void Draw()
    {
        Console.WriteLine("Кнопка в стиле Fluent (Windows 11)");
    }
}

public class FluentCheckBox : ICheckBox
{
    public string ThemeName => "Fluent";

    public void Draw()
    {
        Console.WriteLine("Чекбокс в стиле Fluent (Windows 11)");
    }
}

public class FluentDialogRenderer : IDialogRenderer
{
    public string ThemeName => "Fluent";

    public void RenderDialog(string title)
    {
        Console.WriteLine($"Диалог Fluent: {title}");
    }
}

public class FluentFontEngine : IFontEngine
{
    public string ThemeName => "Fluent";

    public void DrawText(string text)
    {
        Console.WriteLine($"Текст шрифтом Fluent: {text}");
    }
}
