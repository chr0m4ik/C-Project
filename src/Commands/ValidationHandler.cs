using UIFramework.Models;

namespace UIFramework.Commands;

public class ValidationHandler : BaseUIEventHandler
{
    protected override bool TryHandle(UIEvent uiEvent)
    {
        if (string.IsNullOrWhiteSpace(uiEvent.TargetComponentId))
        {
            Console.WriteLine("ValidationHandler: пустой TargetComponentId, событие отклонено");
            return true;
        }

        return false;
    }
}
