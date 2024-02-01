using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;

namespace DigiDent.ClinicManagement.Domain.Visits.Abstractions;

public interface IPastVisitsRepository
    : IVisitsRepository<PastVisit, PastVisitId, Guid>
{
    Task<IReadOnlyCollection<PastVisit>> GetAllAsync(
        CancellationToken cancellationToken);
}