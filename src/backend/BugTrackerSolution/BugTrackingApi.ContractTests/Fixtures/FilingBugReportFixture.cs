using Alba.Security;
using BugTrackerApi.Services;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Testcontainers.PostgreSql;

namespace BugTrackingApi.ContractTests.Fixtures;
public class FilingBugReportFixture : BaseAlbaFixture
{
    public static DateTimeOffset AssumedTime = new(new DateTime(1969, 4, 20, 23, 59, 59), TimeSpan.FromHours(-4));
    private readonly string PG_IMAGE = "postgres:15.2-bullseye";
    private readonly PostgreSqlContainer _pgContainer;
    public FilingBugReportFixture()
    {
        _pgContainer = new PostgreSqlBuilder()
            .WithUsername("postgres")
            .WithPassword("password")
            .WithImage(PG_IMAGE).Build();

    }

    protected override async Task Initializeables()
    {
        await _pgContainer.StartAsync();
        // Need to tell it to use THIS container instead of the one in our appsetting.development.json
        Environment.SetEnvironmentVariable("ConnectionStrings__bugs", _pgContainer.GetConnectionString());

    }

    protected override async Task Disposables()
    {
        await _pgContainer.DisposeAsync().AsTask();
        Environment.SetEnvironmentVariable("ConnectionStrings__bugs", null);
        // Use whatever database library to delete whatever was created by this "collection" of tests.

    }

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
