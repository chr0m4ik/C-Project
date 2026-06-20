using CrossPlatformUISimulator.Patterns.ChainOfResponsibility;
using CrossPlatformUISimulator.Patterns.Command;
using Xunit;

namespace CrossPlatformUISimulator.Tests;

public class ChainAndCommandTests
{
    [Fact]
    public void Chain_ReachesFallbackHandler()
    {
        IUIEventHandler chain = new ValidationHandler();
        chain.SetNext(new RoutingHandler()).SetNext(new FallbackHandler());

        var result = chain.Handle(new UIEvent("Click", "btn1", null, DateTime.Now));

        Assert.True(result);
    }

    [Fact]
    public void ValidationHandler_RejectsEmptyTarget()
    {
        IUIEventHandler chain = new ValidationHandler();
        chain.SetNext(new FallbackHandler());

        var result = chain.Handle(new UIEvent("Click", "", null, DateTime.Now));

        Assert.True(result);
    }

    [Fact]
    public void MoveComponentCommand_ExecuteThenUndo_RestoresPosition()
    {
        var component = new MovableComponent("btn1", 0, 0);
        var command = new MoveComponentCommand(component, 100, 100);

        command.Execute();
        Assert.Equal(100, component.X);

        command.Undo();
        Assert.Equal(0, component.X);
    }

    [Fact]
    public void MoveComponentCommand_ThrowsWhenLocked()
    {
        var component = new MovableComponent("btn1", 0, 0) { IsLocked = true };
        var command = new MoveComponentCommand(component, 50, 50);

        Assert.Throws<InvalidOperationException>(() => command.Execute());
    }

    [Fact]
    public void CommandManager_NewExecute_ClearsRedo()
    {
        var component = new MovableComponent("btn1", 0, 0);
        var manager = new CommandManager();

        manager.Execute(new MoveComponentCommand(component, 10, 10));
        manager.Undo();
        manager.Execute(new MoveComponentCommand(component, 20, 20));

        manager.Redo();
        Assert.Equal(20, component.X);
    }

    [Fact]
    public void MacroCommand_RollsBackOnError()
    {
        var component = new MovableComponent("btn1", 0, 0);
        var lockedComponent = new MovableComponent("btn2", 5, 5) { IsLocked = true };

        var macro = new MacroCommand(new List<IUICommand>
        {
            new MoveComponentCommand(component, 100, 100),
            new MoveComponentCommand(lockedComponent, 200, 200)
        });

        Assert.Throws<InvalidOperationException>(() => macro.Execute());
        Assert.Equal(0, component.X);
    }
}
