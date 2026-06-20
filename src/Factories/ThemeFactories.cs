using UIFramework.Interfaces;
using UIFramework.Models;

namespace UIFramework.Factories;

public class FluentThemeFactory : IThemeFactory
{
    public string ThemeName => "Fluent";

    public IButton CreateButton() => new FluentButton();
    public ICheckBox CreateCheckBox() => new FluentCheckBox();
    public IDialogRenderer CreateDialogRenderer() => new FluentDialogRenderer();
    public IFontEngine CreateFontEngine() => new FluentFontEngine();
}

public class CupertinoThemeFactory : IThemeFactory
{
    public string ThemeName => "Cupertino";

    public IButton CreateButton() => new CupertinoButton();
    public ICheckBox CreateCheckBox() => new CupertinoCheckBox();
    public IDialogRenderer CreateDialogRenderer() => new CupertinoDialogRenderer();
    public IFontEngine CreateFontEngine() => new CupertinoFontEngine();
}
