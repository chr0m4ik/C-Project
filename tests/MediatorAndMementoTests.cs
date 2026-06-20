using CrossPlatformUISimulator.Patterns.Mediator;
using CrossPlatformUISimulator.Patterns.Memento;
using Xunit;

namespace CrossPlatformUISimulator.Tests;

public class MediatorAndMementoTests
{
    [Fact]
    public void Mediator_NotifiesOtherComponentsOnly()
    {
        var mediator = new EventDrivenMediator();
        var button = new MediatorButton("btn1", mediator);
        var panel = new MediatorPanel("panel1");

        mediator.Register(button);
        mediator.Register(panel);

        var exception = Record.Exception(() => button.Click());
        Assert.Null(exception);
    }

    [Fact]
    public void Mediator_Unregister_RemovesComponent()
    {
        var mediator = new EventDrivenMediator();
        var panel = new MediatorPanel("panel1");

        mediator.Register(panel);
        mediator.Unregister(panel);

        var exception = Record.Exception(() => mediator.Notify("someone", new MediatorEvent("someone", "Test", null)));
        Assert.Null(exception);
    }

    [Fact]
    public void Memento_RestoreReturnsOldState()
    {
        var window = new UIWindow { Title = "Окно", X = 10, Y = 10 };
        var snapshot = window.CreateMemento();

        window.X = 500;
        window.Restore(snapshot);

        Assert.Equal(10, window.X);
    }

    [Fact]
    public void MementoManager_RespectsMaxCheckpoints()
    {
        var manager = new UIMementoManager(maxCheckpoints: 2);
        var window = new UIWindow { Title = "Окно" };

        manager.SaveCheckpoint("v1", window.CreateMemento());
        manager.SaveCheckpoint("v2", window.CreateMemento());
        manager.SaveCheckpoint("v3", window.CreateMemento());

        Assert.Equal(2, manager.Count);
    }
}
