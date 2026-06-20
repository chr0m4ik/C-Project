namespace UIFramework.Interfaces;

public interface IThemeFactory
{
    string ThemeName { get; }
    IButton CreateButton();
    ICheckBox CreateCheckBox();
    IDialogRenderer CreateDialogRenderer();
    IFontEngine CreateFontEngine();
}
