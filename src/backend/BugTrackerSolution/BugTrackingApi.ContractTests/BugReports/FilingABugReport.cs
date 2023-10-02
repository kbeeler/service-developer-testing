using Alba;
using BugTrackerApi.Models;

namespace BugTrackingApi.ContractTests.BugReports;
public class FilingABugReport
{
    [Fact]
    public async Task FilingANewBugReport()
    {
        // Given
        var host = await AlbaHost.For<Program>();

        var request = new BugReportCreateRequest
        {
            Description = "Excel Goes Boom",
            Narrative = "A big long thing with steps to reproduce"
        };
        var expectedReponse = new BugReportCreateResponse
        {
            Id = "excel-goes-boom",
            User = "Bob",
            Issue = request,
            Status = IssueStatus.InTriage,
            Software = "Excel",
            Created = DateTime.UtcNow
        };

        // When (and some then)
        var response = await host.Scenario(api =>
        {
            api.Post.Json(request).ToUrl("/catalog/excel/bugs");
            api.StatusCodeShouldBe(201);
        });

        // Then
        var actualResponse = response.ReadAsJson<BugReportCreateResponse>();
        Assert.NotNull(actualResponse);

        Assert.Equal(expectedReponse, actualResponse);
    }
}
