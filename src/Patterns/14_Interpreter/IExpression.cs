namespace CrossPlatformUISimulator.Patterns.Interpreter;

public class InterpreterContext
{
    public List<string> Log { get; } = new();
}

public interface IExpression
{
    void Interpret(InterpreterContext context);
}
