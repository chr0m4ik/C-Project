namespace UIFramework.Models;

public class ButtonConfig
{
    public required string Text { get; set; }
}

public class IconSource
{
    public required string Path { get; set; }
}

public class Dialog
{
    public string Title { get; set; } = "";
    public IconSource? Icon { get; set; }
    public List<ButtonConfig> Buttons { get; set; } = new();
    public bool HasCheckBox { get; set; }
    public string ThemeName { get; set; } = "";

    public Dialog Clone()
    {
        var copy = new Dialog
        {
            Title = Title,
            Icon = Icon == null ? null : new IconSource { Path = Icon.Path },
            HasCheckBox = HasCheckBox,
            ThemeName = ThemeName
        };

        copy.Buttons = new List<ButtonConfig>();
        foreach (var button in Buttons)
        {
            copy.Buttons.Add(new ButtonConfig { Text = button.Text });
        }

        return copy;
    }

    public void Show()
    {
        Console.WriteLine($"--- Диалог: {Title} (тема {ThemeName}) ---");
        if (Icon != null)
        {
            Console.WriteLine($"Иконка: {Icon.Path}");
        }

        foreach (var button in Buttons)
        {
            Console.WriteLine($"Кнопка: {button.Text}");
        }

        if (HasCheckBox)
        {
            Console.WriteLine("Чекбокс: Больше не показывать");
        }
    }
}
