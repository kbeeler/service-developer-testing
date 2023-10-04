using Alba;
using BugTrackerApi.Models;
using BugTrackingApi.ContractTests.Fixtures;

namespace BugTrackingApi.ContractTests.BugReports;

[Collection("SeededDatabaseCollection")]
public class GettingAllBugReportsForAScenario
{
    public readonly IAlbaHost _host;
    public GettingAllBugReportsForAScenario(SeededDatabaseFixture fixture)
    {
        _host = fixture.AlbaHost;
    }

    [Fact]
    public async Task GetAllBugsForExcel()
    {

        var response = await _host.Scenario(api =>
        {
            api.Get.Url("/catalog/excel/bugs");
            api.StatusCodeShouldBeOk();
        });

        var data = response.ReadAsJson<List<BugReportCreateResponse>>();

        Assert.NotNull(data);

        // TODO: Figure this out. Assert.Equal(3, data.Count);


        // what are you going to look at?

    }
}
