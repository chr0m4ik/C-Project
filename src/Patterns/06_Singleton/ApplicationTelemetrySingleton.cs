namespace CrossPlatformUISimulator.Patterns.Singleton;

public class GlobalUiSettings
{
    public string DefaultTheme { get; set; } = "Fluent";
}

public interface IApplicationTelemetry
{
    void LogOperation(string category, string action);
    IReadOnlyDictionary<string, int> GetOperationCounts();
    GlobalUiSettings GetCurrentSettings();
    void ResetForTesting();
}

public class ApplicationTelemetrySingleton : IApplicationTelemetry
{
    private static readonly Lazy<ApplicationTelemetrySingleton> _instance =
        new(() => new ApplicationTelemetrySingleton());

    public static ApplicationTelemetrySingleton Instance => _instance.Value;

    private readonly object _lock = new();
    private Dictionary<string, int> _operationCounts = new();
    private GlobalUiSettings _settings = new();

    private ApplicationTelemetrySingleton()
    {
    }

    public void LogOperation(string category, string action)
    {
        var key = $"{category}.{action}";

        lock (_lock)
        {
            if (!_operationCounts.ContainsKey(key))
            {
                _operationCounts[key] = 0;
            }

            _operationCounts[key]++;
        }
    }

    public IReadOnlyDictionary<string, int> GetOperationCounts()
    {
        lock (_lock)
        {
            return new Dictionary<string, int>(_operationCounts);
        }
    }

    public GlobalUiSettings GetCurrentSettings()
    {
        lock (_lock)
        {
            return new GlobalUiSettings { DefaultTheme = _settings.DefaultTheme };
        }
    }

    public void ResetForTesting()
    {
        lock (_lock)
        {
            _operationCounts = new Dictionary<string, int>();
            _settings = new GlobalUiSettings();
        }
    }
}
