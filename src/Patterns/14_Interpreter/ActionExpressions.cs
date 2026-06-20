namespace CrossPlatformUISimulator.Patterns.Interpreter;

public class ApplyThemeExpression : IExpression
{
    private readonly string _themeName;

    public ApplyThemeExpression(string themeName)
    {
        _themeName = themeName;
    }

    public void Interpret(InterpreterContext context)
    {
        context.Log.Add($"Применить тему: {_themeName}");
    }
}

public class SetPositionExpression : IExpression
{
    private readonly int _x;
    private readonly int _y;

    public SetPositionExpression(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public void Interpret(InterpreterContext context)
    {
        context.Log.Add($"Установить позицию: ({_x},{_y})");
    }
}

public class LockExpression : IExpression
{
    public void Interpret(InterpreterContext context)
    {
        context.Log.Add("Заблокировать компонент");
    }
}
