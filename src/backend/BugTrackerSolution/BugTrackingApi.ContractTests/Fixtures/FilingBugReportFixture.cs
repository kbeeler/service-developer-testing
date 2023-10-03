using Alba.Security;
using BugTrackerApi.Services;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace BugTrackingApi.ContractTests.Fixtures;
public class FilingBugReportFixture : BaseAlbaFixture
{
    public static DateTimeOffset AssumedTime = new(new DateTime(1969, 4, 20, 23, 59, 59), TimeSpan.FromHours(-4));

    protected override void RegisterServices(IServiceCollection services)
    {
        var fakeClock = Substitute.For<ISystemTime>();
        fakeClock.GetCurrent().Returns(AssumedTime);
        // The "recommendation" is you remove the existing thing first, I have NEVER had a problem when I don't.
        services.AddSingleton<ISystemTime>(fakeClock);

    }
    protected override AuthenticationStub GetStub()
    {
        return base.GetStub().WithName("Steve");
    }

}
