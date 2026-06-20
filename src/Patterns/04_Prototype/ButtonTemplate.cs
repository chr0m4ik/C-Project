namespace CrossPlatformUISimulator.Patterns.Prototype;

public interface IPrototypical<T> where T : class
{
    T Clone();
}

public class ButtonTemplate : IPrototypical<ButtonTemplate>
{
    public string Text { get; set; } = "";
    public List<string> Tags { get; set; } = new();

    public ButtonTemplate Clone()
    {
        var copy = new ButtonTemplate
        {
            Text = Text,
            Tags = new List<string>(Tags)
        };

        return copy;
    }

    public void Print()
    {
        Console.WriteLine($"Кнопка-шаблон: {Text}, теги: {string.Join(", ", Tags)}");
    }
}
