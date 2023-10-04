using Alba.Security;
using BugTrackerApi.Services;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace BugTrackingApi.ContractTests.Fixtures;
public class SeededDatabaseFixture : BaseAlbaFixture
{
    public static DateTimeOffset AssumedTime = new(new DateTime(1969, 4, 20, 23, 59, 59), TimeSpan.FromHours(-4));
    private readonly string PG_IMAGE = "jeffrygonzalez/bug-tracker-oct-2023:3-bugs-in-excel";
    private readonly IContainer _container;
    public SeededDatabaseFixture()
    {
        _container = new ContainerBuilder()

          .WithEnvironment("PGDATA", "/pgdata")
          .WithPortBinding("5432", "5432")
          .WithImage(PG_IMAGE).Build();

    }

    protected override async Task Initializeables()
    {
        await _container.StartAsync();
        // Need to tell it to use THIS container instead of the one in our appsetting.development.json
        //Environment.SetEnvironmentVariable("ConnectionStrings__bugs", "PORT = 5432; HOST = localhost; DATABASE = postgres; PASSWORD = password; USER ID = postgres);
    }
    protected override async Task Disposables()
    {
        await _container.DisposeAsync().AsTask();
        //Environment.SetEnvironmentVariable("ConnectionStrings__bugs", null);
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
