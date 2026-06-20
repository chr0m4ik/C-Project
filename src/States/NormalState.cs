using UIFramework.Interfaces;
using UIFramework.Models;

namespace UIFramework.States;

public class NormalState : IComponentState
{
    public string StateName => "Normal";

    public void Enter(StatefulComponent context)
    {
        Console.WriteLine($"{context.Id} вошёл в состояние Normal");
    }

    public void Exit(StatefulComponent context)
    {
        Console.WriteLine($"{context.Id} выходит из Normal");
    }

    public void HandleClick(StatefulComponent context)
    {
        Console.WriteLine($"{context.Id}: обычный клик обработан");
    }
}
