using UIFramework.Interfaces;
using UIFramework.Models;
using UIFramework.Factories;
using UIFramework.Builders;
using UIFramework.Directors;
using Xunit;

namespace UIFramework.Tests;

public class CreationalPatternsTests
{
    [Fact]
    public void StandardWidgetFactory_CreatesButton()
    {
        IWidgetFactory factory = new StandardWidgetFactory();
        var widget = factory.CreateWidget(new WidgetConfig { Type = WidgetType.Button, Label = "Тест" });

        Assert.IsType<ButtonWidget>(widget);
    }

    [Fact]
    public void DebugWidgetFactory_CreatesDebugButton()
    {
        IWidgetFactory factory = new DebugWidgetFactory();
        var widget = factory.CreateWidget(new WidgetConfig { Type = WidgetType.Button, Label = "Тест" });

        Assert.IsType<DebugButtonWidget>(widget);
    }

    [Fact]
    public void FluentThemeFactory_CreatesFluentComponents()
    {
        IThemeFactory themeFactory = new FluentThemeFactory();
        var button = themeFactory.CreateButton();

        Assert.Equal("Fluent", button.ThemeName);
    }

    [Fact]
    public void DialogBuilder_ThrowsWithoutTitle()
    {
        var builder = new DialogBuilder();
        builder.AddButton(new ButtonConfig { Text = "OK" }).SetTheme("Fluent");

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    [Fact]
    public void DialogBuilder_ThrowsWithoutButtons()
    {
        var builder = new DialogBuilder();
        builder.SetTitle("Заголовок").SetTheme("Fluent");

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    [Fact]
    public void ErrorDialogDirector_BuildsValidDialog()
    {
        var director = new ErrorDialogDirector();
        var dialog = director.Build(new DialogBuilder(), "Fluent");

        Assert.Equal("Ошибка", dialog.Title);
        Assert.Single(dialog.Buttons);
    }

    [Fact]
    public void Dialog_Clone_IsIndependent()
    {
        var director = new ErrorDialogDirector();
        var dialog = director.Build(new DialogBuilder(), "Fluent");

        var clone = dialog.Clone();
        clone.Title = "Изменено";

        Assert.NotEqual(dialog.Title, clone.Title);
    }

    [Fact]
    public void ButtonTemplate_Clone_IsIndependent()
    {
        var original = new ButtonTemplate { Text = "Оригинал" };
        original.Tags.Add("primary");

        var clone = original.Clone();
        clone.Text = "Клон";
        clone.Tags.Add("secondary");

        Assert.Equal("Оригинал", original.Text);
        Assert.Single(original.Tags);
        Assert.Equal(2, clone.Tags.Count);
    }

    [Fact]
    public void LegacyEngineAdapter_DrawButton_DoesNotThrow()
    {
        var adapter = new LegacyEngineAdapter(new LegacyGraphicsEngine());

        var exception = Record.Exception(() => adapter.DrawButton(1, 2, 3, 4, "Тест"));

        Assert.Null(exception);
    }

    [Fact]
    public void Singleton_AlwaysReturnsSameInstance()
    {
        var first = ApplicationTelemetrySingleton.Instance;
        var second = ApplicationTelemetrySingleton.Instance;

        Assert.Same(first, second);
    }

    [Fact]
    public void Singleton_ResetForTesting_ClearsCounts()
    {
        ApplicationTelemetrySingleton.Instance.ResetForTesting();
        ApplicationTelemetrySingleton.Instance.LogOperation("Test", "Action");

        var counts = ApplicationTelemetrySingleton.Instance.GetOperationCounts();
        Assert.Single(counts);

        ApplicationTelemetrySingleton.Instance.ResetForTesting();
        var countsAfterReset = ApplicationTelemetrySingleton.Instance.GetOperationCounts();
        Assert.Empty(countsAfterReset);
    }
}
