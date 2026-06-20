using UIFramework.Interfaces;
using UIFramework.Models;
using UIFramework.Builders;
using UIFramework.Directors;

namespace UIFramework.Integration;

public class UISystemFacade
{
    private readonly IThemeFactory _themeFactory;
    private readonly IApplicationTelemetry _telemetry;
    private readonly ContainerComponent _rootTree;

    public UISystemFacade(IThemeFactory themeFactory, IApplicationTelemetry telemetry)
    {
        _themeFactory = themeFactory;
        _telemetry = telemetry;
        _rootTree = new ContainerComponent("root");
    }

    public Dialog CreateDialog(string title)
    {
        var builder = new DialogBuilder();
        var director = new ErrorDialogDirector();
        var dialog = director.Build(builder, _themeFactory.ThemeName);

        _telemetry.LogOperation("Facade", "CreateDialog");
        return dialog;
    }

    public void AddComponentToTree(IUIComponent component)
    {
        _rootTree.AddChild(component);
    }

    public void RenderAll()
    {
        _rootTree.Render();
        _telemetry.LogOperation("Facade", "RenderAll");
    }

    public void LogCurrentMetrics()
    {
        var counts = _telemetry.GetOperationCounts();
        foreach (var pair in counts)
        {
            Console.WriteLine($"Метрика: {pair.Key} = {pair.Value}");
        }
    }
}
