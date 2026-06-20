using UIFramework.Models;

namespace UIFramework.Interfaces;

public interface IComponentState
{
    string StateName { get; }
    void Enter(StatefulComponent context);
    void Exit(StatefulComponent context);
    void HandleClick(StatefulComponent context);
}
