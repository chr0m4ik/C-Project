namespace CrossPlatformUISimulator.Patterns.Interpreter;

public class ChainExpression : IExpression
{
    private readonly List<IExpression> _actions;

    public ChainExpression(List<IExpression> actions)
    {
        _actions = actions;
    }

    public void Interpret(InterpreterContext context)
    {
        foreach (var action in _actions)
        {
            action.Interpret(context);
        }
    }
}
