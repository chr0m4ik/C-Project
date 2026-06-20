using CrossPlatformUISimulator.Patterns.Observer;
using CrossPlatformUISimulator.Patterns.State;
using Xunit;

namespace CrossPlatformUISimulator.Tests;

public class ObserverAndStateTests
{
    private class CountingObserver : IUIStateObserver
    {
        public int CallCount { get; private set; }

        public void OnStateChange(string componentId, UIStateChangeData data)
        {
            CallCount++;
        }
    }

    [Fact]
    public void Subject_NotifiesAllAttachedObservers()
    {
        var subject = new SafeObserverSubject();
        var observer1 = new CountingObserver();
        var observer2 = new CountingObserver();

        subject.Attach(observer1);
        subject.Attach(observer2);
        subject.Notify("comp1", new UIStateChangeData("X", "0", "1", DateTime.Now));

        Assert.Equal(1, observer1.CallCount);
        Assert.Equal(1, observer2.CallCount);
    }

    [Fact]
    public void Subject_DetachStopsNotifications()
    {
        var subject = new SafeObserverSubject();
        var observer = new CountingObserver();

        subject.Attach(observer);
        subject.Detach(observer);
        subject.Notify("comp1", new UIStateChangeData("X", "0", "1", DateTime.Now));

        Assert.Equal(0, observer.CallCount);
    }

    [Fact]
    public void StatefulComponent_StartsInNormalState()
    {
        var component = new StatefulComponent("btn1");

        Assert.Equal("Normal", component.CurrentStateName);
    }

    [Fact]
    public void StatefulComponent_TransitionTo_ChangesState()
    {
        var component = new StatefulComponent("btn1");

        component.TransitionTo(new LoadingState());

        Assert.Equal("Loading", component.CurrentStateName);
    }
}
