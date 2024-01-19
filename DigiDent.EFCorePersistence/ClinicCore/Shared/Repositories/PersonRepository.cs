
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;

namespace DigiDent.EFCorePersistence.ClinicCore.Shared.Repositories;

public class PersonRepository: IPersonRepository
{
    private readonly ClinicCoreDbContext _context;

    public PersonRepository(ClinicCoreDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync<TPerson, TPersonId>(
        TPerson person,
        CancellationToken cancellationToken)
        where TPerson : class, IPerson<TPersonId>
        where TPersonId : IPersonId
    {
        await _context
            .Set<TPerson>()
            .AddAsync(person, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}