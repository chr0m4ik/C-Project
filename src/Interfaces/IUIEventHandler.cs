using UIFramework.Models;

namespace UIFramework.Interfaces;

public interface IUIEventHandler
{
    IUIEventHandler SetNext(IUIEventHandler next);
    bool Handle(UIEvent uiEvent);
}
