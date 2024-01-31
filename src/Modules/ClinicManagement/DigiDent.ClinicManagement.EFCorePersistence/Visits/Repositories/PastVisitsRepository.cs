using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.EFCorePersistence.ClinicCore.Visits.Repositories;

public class PastVisitsRepository :
    VisitsRepository<PastVisit, PastVisitId, Guid>, IPastVisitsRepository
{
    private readonly ClinicCoreDbContext _context;
    public PastVisitsRepository(ClinicCoreDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<PastVisit>> GetAllAsync(CancellationToken cancellationToken)
    {
        return (await _context.PastVisits
                .Include(pv => pv.Doctor)
                .Include(pv => pv.Patient)
                .ToListAsync(cancellationToken))
            .AsReadOnly();
    }
}