using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;

namespace DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;

public interface IPastVisitsRepository
    : IVisitsRepository<PastVisit, PastVisitId, Guid>
{
    Task<IReadOnlyCollection<PastVisit>> GetAllAsync(
        CancellationToken cancellationToken);
}