using UIFramework.Interfaces;

namespace UIFramework.Models;

public class MediatorButton : IMediatorComponent
{
    public string Id { get; }
    private readonly IUIComponentMediator _mediator;

    public MediatorButton(string id, IUIComponentMediator mediator)
    {
        Id = id;
        _mediator = mediator;
    }

    public void Click()
    {
        Console.WriteLine($"Кнопка {Id} нажата");
        _mediator.Notify(Id, new MediatorEvent(Id, "Click", null));
    }

    public void Receive(MediatorEvent mediatorEvent)
    {
        Console.WriteLine($"Кнопка {Id} получила событие {mediatorEvent.EventType} от {mediatorEvent.SenderId}");
    }
}

public class MediatorPanel : IMediatorComponent
{
    public string Id { get; }

    public MediatorPanel(string id)
    {
        Id = id;
    }

    public void Receive(MediatorEvent mediatorEvent)
    {
        Console.WriteLine($"Панель {Id} получила событие {mediatorEvent.EventType} от {mediatorEvent.SenderId}");
    }
}
