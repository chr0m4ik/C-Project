using UIFramework.Interfaces;
using UIFramework.Models;

namespace UIFramework.Directors;

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
