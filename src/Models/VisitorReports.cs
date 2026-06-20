namespace UIFramework.Models;

public class MetricsReport
{
    public int ButtonCount { get; set; }
    public int PanelCount { get; set; }
    public int DialogCount { get; set; }
}

public class ValidationViolation
{
    public required string ComponentId { get; set; }
    public required string Reason { get; set; }
}
