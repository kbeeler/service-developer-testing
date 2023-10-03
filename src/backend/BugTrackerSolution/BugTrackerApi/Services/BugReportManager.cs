using BugTrackerApi.Models;
using Marten;
using OneOf;
using SlugUtils;

namespace BugTrackerApi.Services;

public class BugReportManager
{
    private readonly SoftwareCatalogManager _softwareCatalog;
    private readonly ISystemTime _systemTime;
    private readonly SlugUtils.SlugGenerator _slugGenerator;
    private readonly IDocumentSession _documentSession;

    public BugReportManager(SoftwareCatalogManager softwareCatalog, ISystemTime systemTime, SlugGenerator slugGenerator, IDocumentSession documentSession)
    {
        _softwareCatalog = softwareCatalog;
        _systemTime = systemTime;
        _slugGenerator = slugGenerator;
        _documentSession = documentSession;
    }




    // CreateBugReportAsync(user, software, request);
    public async Task<OneOf<BugReportCreateResponse, SoftwareNotInCatalog>> CreateBugReportAsync(string user, string software, BugReportCreateRequest request)
    {
        var softwareLookup = await _softwareCatalog.IsSofwareInOurCatalogAsync(software);

        if (softwareLookup.TryPickT0(out SoftwareEntity entity, out SoftwareNotInCatalog notFound))
        {
            if (entity is not null)
            {
                var report = new BugReportCreateResponse
                {
                    Created = _systemTime.GetCurrent(),
                    Id = await _slugGenerator.GenerateSlugAsync(request.Description, CheckForUniqueAsync),
                    Issue = request,
                    Software = entity.Name,
                    Status = IssueStatus.InTriage,
                    User = user
                };

                var entityToSave = new BugReportEntity
                {
                    Id = Guid.NewGuid(),
                    BugReport = report,
                };
                _documentSession.Insert(entityToSave);
                await _documentSession.SaveChangesAsync();

                return report;
            }

        }
        return new SoftwareNotInCatalog();

        async Task<bool> CheckForUniqueAsync(string slug)
        {
            return await _documentSession.Query<BugReportEntity>().Where(b => b.BugReport.Id == slug).AnyAsync() == false;
        }

    }
}


public record SoftwareNotInCatalog();

public class BugReportEntity
{
    public Guid Id { get; set; }
    public BugReportCreateResponse BugReport { get; set; } = new();
}