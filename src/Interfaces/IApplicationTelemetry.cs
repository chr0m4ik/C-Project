namespace UIFramework.Interfaces;

public interface IApplicationTelemetry
{
    void LogOperation(string category, string action);
    IReadOnlyDictionary<string, int> GetOperationCounts();
    void ResetForTesting();
}
