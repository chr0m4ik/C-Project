using UIFramework.Interfaces;
using UIFramework.Models;

namespace UIFramework.States;

public class ErrorState : IComponentState
{
    public string StateName => "Error";

    public void Enter(StatefulComponent context)
    {
        Console.WriteLine($"{context.Id} вошёл в состояние Error");
    }

    public void Exit(StatefulComponent context)
    {
        Console.WriteLine($"{context.Id} выходит из Error");
    }

    public void HandleClick(StatefulComponent context)
    {
        Console.WriteLine($"{context.Id}: требуется Reset перед обработкой клика");
    }
}
