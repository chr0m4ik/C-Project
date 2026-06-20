using UIFramework.Interfaces;
using UIFramework.Models;

namespace UIFramework.States;

public class LoadingState : IComponentState
{
    public string StateName => "Loading";

    public void Enter(StatefulComponent context)
    {
        Console.WriteLine($"{context.Id} вошёл в состояние Loading");
    }

    public void Exit(StatefulComponent context)
    {
        Console.WriteLine($"{context.Id} выходит из Loading");
    }

    public void HandleClick(StatefulComponent context)
    {
        Console.WriteLine($"{context.Id}: клик игнорируется во время загрузки");
    }
}
