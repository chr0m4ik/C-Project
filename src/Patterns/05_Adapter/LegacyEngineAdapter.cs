namespace CrossPlatformUISimulator.Patterns.Adapter;

public interface IModernRenderer
{
    void DrawButton(int x, int y, int width, int height, string label);
    void DrawText(string text, int x, int y);
    void ShowDialog(string title);
}

public class LegacyEngineAdapter : IModernRenderer
{
    private readonly LegacyGraphicsEngine _legacyEngine;

    public LegacyEngineAdapter(LegacyGraphicsEngine legacyEngine)
    {
        _legacyEngine = legacyEngine;
    }

    public void DrawButton(int x, int y, int width, int height, string label)
    {
        _legacyEngine.DrawNativeButton(x, y, width, height, label);
    }

    public void DrawText(string text, int x, int y)
    {
        _legacyEngine.RenderTextRaster("Arial", 12, 0, 0, 0, x, y, text);
    }

    public void ShowDialog(string title)
    {
        _legacyEngine.ShowModalWindow(title, true);
    }
}
