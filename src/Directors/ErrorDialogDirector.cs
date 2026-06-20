using UIFramework.Interfaces;
using UIFramework.Models;

namespace UIFramework.Directors;

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
