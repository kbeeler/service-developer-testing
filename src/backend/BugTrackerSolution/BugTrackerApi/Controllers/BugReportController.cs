using BugTrackerApi.Models;
using BugTrackerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackerApi.Controllers;

public class BugReportController : ControllerBase
{

    private readonly BugReportManager _bugManager;

    public BugReportController(BugReportManager bugManager)
    {
        _bugManager = bugManager;
    }

    [Authorize]
    [HttpPost("/catalog/{software}/bugs")]
    public async Task<ActionResult<BugReportCreateResponse>> AddABugReport([FromBody] BugReportCreateRequest request, [FromRoute] string software)
    {
        var slugGenerator = new SlugUtils.SlugGenerator();
        var user = User.GetName();
        var response = await _bugManager.CreateBugReportAsync(user, software, request);

        return response.Match<ActionResult>(
            report => StatusCode(201, report),
            _ => NotFound("That software is not supported")
            );
    }

}
