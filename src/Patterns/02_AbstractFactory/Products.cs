namespace CrossPlatformUISimulator.Patterns.AbstractFactory;

public interface IThemeMarker
{
    string ThemeName { get; }
}

public interface IButton : IThemeMarker
{
    void Draw();
}

public interface ICheckBox : IThemeMarker
{
    void Draw();
}

public interface IDialogRenderer : IThemeMarker
{
    void RenderDialog(string title);
}

public interface IFontEngine : IThemeMarker
{
    void DrawText(string text);
}
