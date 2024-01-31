using DigiDent.ClinicManagement.Domain.Visits.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.ClinicManagement.EFCorePersistence.Visits.Repositories;

public abstract class VisitsRepository<TVisit, TId, TIdValue> :
    IVisitsRepository<TVisit, TId, TIdValue>
    where TVisit: class, IVisit<TId, TIdValue>
    where TId: class, IVisitId<TIdValue>
    where TIdValue: notnull
{
    private readonly ClinicCoreDbContext _context;
    
    protected VisitsRepository(ClinicCoreDbContext context)
    {
        _context = context;
    }
    
    public virtual async Task<TVisit?> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        return await _context.Set<TVisit>()
            .Include(v => v.Doctor)
            .Include(v => v.Patient)
            .Include(v => v.TreatmentPlan)
            .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task AddAsync(TVisit visit, CancellationToken cancellationToken)
    {
        await _context.Set<TVisit>().AddAsync(visit, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}