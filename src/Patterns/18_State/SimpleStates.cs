namespace CrossPlatformUISimulator.Patterns.State;

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
