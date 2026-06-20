using CrossPlatformUISimulator.Patterns.Adapter;
using CrossPlatformUISimulator.Patterns.Singleton;
using Xunit;

namespace CrossPlatformUISimulator.Tests;

public class AdapterAndSingletonTests
{
    [Fact]
    public void LegacyEngineAdapter_DrawButton_DoesNotThrow()
    {
        var adapter = new LegacyEngineAdapter(new LegacyGraphicsEngine());

        var exception = Record.Exception(() => adapter.DrawButton(1, 2, 3, 4, "Тест"));

        Assert.Null(exception);
    }

    [Fact]
    public void Singleton_AlwaysReturnsSameInstance()
    {
        var first = ApplicationTelemetrySingleton.Instance;
        var second = ApplicationTelemetrySingleton.Instance;

        Assert.Same(first, second);
    }

    [Fact]
    public void Singleton_ResetForTesting_ClearsCounts()
    {
        ApplicationTelemetrySingleton.Instance.ResetForTesting();
        ApplicationTelemetrySingleton.Instance.LogOperation("Test", "Action");

        var counts = ApplicationTelemetrySingleton.Instance.GetOperationCounts();
        Assert.Single(counts);

        ApplicationTelemetrySingleton.Instance.ResetForTesting();
        var countsAfterReset = ApplicationTelemetrySingleton.Instance.GetOperationCounts();
        Assert.Empty(countsAfterReset);
    }

    [Fact]
    public void Singleton_ParallelCalls_ReturnSameInstance()
    {
        ApplicationTelemetrySingleton? first = null;
        ApplicationTelemetrySingleton? second = null;

        Parallel.Invoke(
            () => first = ApplicationTelemetrySingleton.Instance,
            () => second = ApplicationTelemetrySingleton.Instance
        );

        Assert.Same(first, second);
    }
}
