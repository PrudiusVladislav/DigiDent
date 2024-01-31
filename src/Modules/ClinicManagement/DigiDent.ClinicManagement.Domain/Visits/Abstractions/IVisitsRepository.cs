namespace DigiDent.ClinicManagement.Domain.Visits.Abstractions;

public interface IVisitsRepository<TVisit, TId, TIdValue>
    where TVisit: class, IVisit<TId, TIdValue>
    where TId: class, IVisitId<TIdValue>
    where TIdValue: notnull
{
    Task<TVisit?> GetByIdAsync(TId id, CancellationToken cancellationToken);
    Task AddAsync(TVisit visit, CancellationToken cancellationToken);
}