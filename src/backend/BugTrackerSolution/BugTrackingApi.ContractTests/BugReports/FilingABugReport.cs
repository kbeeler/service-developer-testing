using Alba;
using BugTrackerApi.Models;
using BugTrackingApi.ContractTests.Fixtures;

namespace BugTrackingApi.ContractTests.BugReports;

[Collection("FilingABugReport")]
public class FilingABugReport
{

    private readonly IAlbaHost _host;
    public FilingABugReport(FilingBugReportFixture fixture)
    {
        _host = fixture.AlbaHost;
    }
    [Fact]
    public async Task FilingANewBugReport()
    {
        // Given


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
            Created = FilingBugReportFixture.AssumedTime
        };

        // When (and some then)
        var response = await _host.Scenario(api =>
        {
            api.Post.Json(request).ToUrl("/catalog/excel/bugs");
            api.StatusCodeShouldBe(201);
            // should be checking for the location here.
        });

        // Then
        var actualResponse = response.ReadAsJson<BugReportCreateResponse>();
        Assert.NotNull(actualResponse);

        Assert.Equal(expectedReponse, actualResponse);
    }
}
