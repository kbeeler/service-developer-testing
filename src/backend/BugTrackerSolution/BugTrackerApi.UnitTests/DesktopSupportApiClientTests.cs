using BugTrackerApi.Services;
using NSubstitute;

namespace BugTrackerApi.UnitTests;
public class DesktopSupportApiClientTests
{
    [Fact]
    public async Task TheTest()
    {

        var fakeHandler = Substitute.For<HttpMessageHandler>();
        var client = new HttpClient(fakeHandler); // come back to this.

        var desktopSupport = new DesktopSupportHttpClient(client);

        var response = await desktopSupport.SendSupportTicketAsync(new SupportTicketRequest()
        {
            Software = "excel",
            User = "paul"
        });




    }
}
