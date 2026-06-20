using UIFramework.Models;

namespace UIFramework.Interfaces;

public interface IMediatorComponent
{
    string Id { get; }
    void Receive(MediatorEvent mediatorEvent);
}

public interface IUIComponentMediator
{
    void Register(IMediatorComponent component);
    void Unregister(IMediatorComponent component);
    void Notify(string senderId, MediatorEvent mediatorEvent);
}
