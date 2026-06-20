using UIFramework.Models;

namespace UIFramework.Integration;

public interface IExpression
{
    void Interpret(InterpreterContext context);
}
