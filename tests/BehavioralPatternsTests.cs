using UIFramework.Interfaces;
using UIFramework.Models;
using UIFramework.Commands;
using UIFramework.Integration;
using UIFramework.Observers;
using UIFramework.States;
using UIFramework.Strategies;
using UIFramework.Visitors;
using Xunit;

namespace UIFramework.Tests;

public class BehavioralPatternsTests
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

    [Fact]
    public void DepthFirstIterator_VisitsAllNodes()
    {
        var root = new ContainerComponent("root");
        root.AddChild(new LeafComponent("a", "A"));
        root.AddChild(new LeafComponent("b", "B"));

        var iterator = new DepthFirstIterator(root);
        int count = 0;
        while (iterator.MoveNext())
        {
            count++;
        }

        Assert.Equal(3, count);
    }

    [Fact]
    public void DslParser_ParsesChainCorrectly()
    {
        var parser = new DslParser();
        var expression = parser.Parse("EXECUTE ApplyTheme('Fluent') -> Lock()");
        var context = new InterpreterContext();

        expression.Interpret(context);

        Assert.Equal(2, context.Log.Count);
    }

    [Fact]
    public void DslParser_ThrowsOnEmptyScript()
    {
        var parser = new DslParser();

        Assert.Throws<ParseException>(() => parser.Parse(""));
    }

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

    [Fact]
    public void Subject_NotifiesAllAttachedObservers()
    {
        var subject = new SafeObserverSubject();
        var calls = 0;
        var observer = new CountingObserver(() => calls++);

        subject.Attach(observer);
        subject.Notify("comp1", new UIStateChangeData("X", "0", "1", DateTime.Now));

        Assert.Equal(1, calls);
    }

    [Fact]
    public void StatefulComponent_TransitionTo_ChangesState()
    {
        var component = new StatefulComponent("btn1");

        component.TransitionTo(new LoadingState());

        Assert.Equal("Loading", component.CurrentStateName);
    }

    [Fact]
    public void StackLayoutStrategy_PlacesComponentsVertically()
    {
        var strategy = new StackLayoutStrategy();
        var context = new LayoutContext(Padding: 0, Spacing: 0, AvailableWidth: 100);

        var result = strategy.CalculateBounds(new List<string> { "a", "b" }, context);

        Assert.True(result[1].Y > result[0].Y);
    }

    [Fact]
    public void TemplateMethod_ValidationFailed_SkipsRender()
    {
        var lifecycle = new ComplexContainerLifecycle(new List<string>());

        var exception = Record.Exception(() => lifecycle.ExecuteLifecycle(new UIContext { ComponentId = "empty" }));

        Assert.Null(exception);
    }

    [Fact]
    public void MetricsCollectorVisitor_CountsButtons()
    {
        var panel = new VisitablePanel("panel1");
        panel.Children.Add(new VisitableButton("btn1"));
        panel.Children.Add(new VisitableButton("btn2"));

        var visitor = new MetricsCollectorVisitor();
        panel.Accept(visitor);

        Assert.Equal(2, visitor.Report.ButtonCount);
    }

    [Fact]
    public void DependencyValidatorVisitor_DetectsDuplicateId()
    {
        var panel = new VisitablePanel("panel1");
        panel.Children.Add(new VisitableButton("sameId"));
        panel.Children.Add(new VisitableButton("sameId"));

        var visitor = new DependencyValidatorVisitor();
        panel.Accept(visitor);

        Assert.Single(visitor.Violations);
    }

    private class CountingObserver : IUIStateObserver
    {
        private readonly Action _onCall;

        public CountingObserver(Action onCall)
        {
            _onCall = onCall;
        }

        public void OnStateChange(string componentId, UIStateChangeData data)
        {
            _onCall();
        }
    }
}
