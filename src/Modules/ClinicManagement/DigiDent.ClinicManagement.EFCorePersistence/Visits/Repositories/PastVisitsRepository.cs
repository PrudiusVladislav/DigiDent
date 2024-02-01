using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.ClinicManagement.Domain.Visits.Abstractions;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.ClinicManagement.EFCorePersistence.Visits.Repositories;

public class PastVisitsRepository :
    VisitsRepository<PastVisit, PastVisitId, Guid>, IPastVisitsRepository
{
    private readonly ClinicManagementDbContext _context;
    public PastVisitsRepository(ClinicManagementDbContext context)
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