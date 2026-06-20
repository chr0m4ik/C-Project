namespace UIFramework.Interfaces;

public interface IRenderingStrategy
{
    void DrawBackground(string componentName);
    void DrawBorder(string componentName);
    void DrawText(string text);
}
