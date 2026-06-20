namespace CrossPlatformUISimulator.Patterns.Builder;

public class ErrorDialogDirector
{
    public Dialog Build(IDialogBuilder builder, string themeName)
    {
        return builder
            .SetTitle("Ошибка")
            .SetIcon(new IconSource { Path = "error_icon.png" })
            .AddButton(new ButtonConfig { Text = "OK" })
            .SetTheme(themeName)
            .Build();
    }
}

public class MultiChoiceDialogDirector
{
    public Dialog Build(IDialogBuilder builder, string themeName)
    {
        builder
            .SetTitle("Выберите вариант")
            .AddButton(new ButtonConfig { Text = "Вариант 1" })
            .AddButton(new ButtonConfig { Text = "Вариант 2" })
            .AddButton(new ButtonConfig { Text = "Вариант 3" })
            .SetTheme(themeName)
            .SetHasCheckBox(true);

        return builder.Build();
    }
}
