using BugTrackerApi.Models;
using BugTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackerApi.Controllers;

public class BugReportController : ControllerBase
{
    private readonly ISystemTime _systemTime;

    public BugReportController(ISystemTime systemTime)
    {
        _systemTime = systemTime;
    }

    [HttpPost("/catalog/excel/bugs")]
    public async Task<ActionResult> AddABugReport([FromBody] BugReportCreateRequest request)
    {
        var response = new BugReportCreateResponse
        {
            Id = "excel-goes-boom",
            Issue = request,
            Software = "Excel",
            Status = IssueStatus.InTriage,
            User = "Bob",
            Created = _systemTime.GetCurrent()
        };
        return StatusCode(201, response);
    }
}
