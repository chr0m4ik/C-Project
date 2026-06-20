using UIFramework.Models;

namespace UIFramework.Interfaces;

public interface IDialogBuilder
{
    IDialogBuilder SetTitle(string title);
    IDialogBuilder AddButton(ButtonConfig config);
    IDialogBuilder SetIcon(IconSource source);
    IDialogBuilder SetTheme(string themeName);
    IDialogBuilder SetHasCheckBox(bool value);
    Dialog Build();
}
