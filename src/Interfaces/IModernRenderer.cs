namespace UIFramework.Interfaces;

public interface IModernRenderer
{
    void DrawButton(int x, int y, int width, int height, string label);
    void DrawText(string text, int x, int y);
    void ShowDialog(string title);
}
