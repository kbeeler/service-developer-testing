using Alba;
using BugTrackerApi.Models;
using BugTrackerApi.Services;
using BugTrackingApi.ContractTests.Fixtures;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace BugTrackingApi.ContractTests.BugReports;

[Collection("FilingABugReport")]
public class FilingAndRetreivingABugReport
{
    private readonly IAlbaHost _host;
    private readonly WireMockServer _mockServer;

    public FilingAndRetreivingABugReport(FilingBugReportFixture fixture)
    {
        _host = fixture.AlbaHost;
        _mockServer = fixture.MockServer;
    }

    [Fact]
    public async Task AddingAndRetrievingABugReort()
    {
        // Given 
        var expectedTicketId = Guid.NewGuid();
        var messageToApi = new SupportTicketRequest
        {
            Software = "excel",
            User = "Steve"
        };
        var messageToApiJson = """
            {
                "software": "excel",
                "user": "Steve"
            }

            """;
        _mockServer.Given(Request.Create().WithPath("/support-tickets"))
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithBodyAsJson(new SupportTicketResponse()
                {
                    TicketId = expectedTicketId,
                    Request = messageToApi
                }));
        var request = new BugReportCreateRequest
        {
            Description = "spell checker broken",
            Narrative = "I can know lownger chek my slepping!"
        };

        var response = await _host.Scenario(api =>
        {
            api.Post.Json(request).ToUrl("/catalog/excel/bugs");
            api.StatusCodeShouldBe(201);
        });

        var firstResponse = response.ReadAsJson<BugReportCreateResponse>();
        Assert.NotNull(firstResponse);

        // When
        var response2 = await _host.Scenario(api =>
        {
            api.Get.Url($"/catalog/excel/bugs/{firstResponse.Id}");
            api.StatusCodeShouldBeOk();
        });

        var secondResponse = response2.ReadAsJson<BugReportCreateResponse>();

        // Then
        Assert.NotNull(secondResponse);

        Assert.Equal(firstResponse, secondResponse);
    }

}
