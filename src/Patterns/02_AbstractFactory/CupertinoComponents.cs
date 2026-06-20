namespace CrossPlatformUISimulator.Patterns.AbstractFactory;

public class CupertinoButton : IButton
{
    public string ThemeName => "Cupertino";

    public void Draw()
    {
        Console.WriteLine("Кнопка в стиле Cupertino (macOS)");
    }
}

public class CupertinoCheckBox : ICheckBox
{
    public string ThemeName => "Cupertino";

    public void Draw()
    {
        Console.WriteLine("Чекбокс в стиле Cupertino (macOS)");
    }
}

public class CupertinoDialogRenderer : IDialogRenderer
{
    public string ThemeName => "Cupertino";

    public void RenderDialog(string title)
    {
        Console.WriteLine($"Диалог Cupertino: {title}");
    }
}

public class CupertinoFontEngine : IFontEngine
{
    public string ThemeName => "Cupertino";

    public void DrawText(string text)
    {
        Console.WriteLine($"Текст шрифтом Cupertino: {text}");
    }
}
