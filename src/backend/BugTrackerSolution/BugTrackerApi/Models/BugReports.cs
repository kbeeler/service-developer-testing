namespace BugTrackerApi.Models;



public record BugReportCreateRequest
{
    public string Description { get; set; } = string.Empty;
    public string Narrative { get; set; } = string.Empty;
}



public record BugReportCreateResponse
{
    public string Id { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string Software { get; set; } = string.Empty;
    public BugReportCreateRequest Issue { get; set; } = new();
    public IssueStatus Status { get; set; }
    public DateTimeOffset Created { get; set; }
}

public enum IssueStatus { InTriage }