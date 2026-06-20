using CrossPlatformUISimulator.Patterns.Builder;
using CrossPlatformUISimulator.Patterns.Prototype;
using Xunit;

namespace CrossPlatformUISimulator.Tests;

public class BuilderAndPrototypeTests
{
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
}
