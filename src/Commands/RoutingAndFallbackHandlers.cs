using UIFramework.Models;

namespace UIFramework.Commands;

public class RoutingHandler : BaseUIEventHandler
{
    protected override bool TryHandle(UIEvent uiEvent)
    {
        Console.WriteLine($"RoutingHandler: событие {uiEvent.EventType} направлено к {uiEvent.TargetComponentId}");
        return false;
    }
}

public class FallbackHandler : BaseUIEventHandler
{
    protected override bool TryHandle(UIEvent uiEvent)
    {
        Console.WriteLine($"FallbackHandler: событие {uiEvent.EventType} обработано по умолчанию");
        return true;
    }
}
