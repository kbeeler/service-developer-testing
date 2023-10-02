using BugTrackerApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackerApi.Controllers;

public class BugReportController : ControllerBase
{

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
            Created = DateTime.UtcNow
        };
        return StatusCode(201, response);
    }
}
