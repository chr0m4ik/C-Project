using UIFramework.Interfaces;

namespace UIFramework.Models;

public class ButtonTemplate : IPrototypical<ButtonTemplate>
{
    public string Text { get; set; } = "";
    public List<string> Tags { get; set; } = new();

    public ButtonTemplate Clone()
    {
        return new ButtonTemplate
        {
            Text = Text,
            Tags = new List<string>(Tags)
        };
    }

    public void Print()
    {
        Console.WriteLine($"Кнопка-шаблон: {Text}, теги: {string.Join(", ", Tags)}");
    }
}
