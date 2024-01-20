
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;

namespace DigiDent.EFCorePersistence.ClinicCore.Shared.Repositories;

public class PersonRepository: IPersonRepository
{
    private readonly ClinicCoreDbContext _context;

    public PersonRepository(ClinicCoreDbContext context)
    {
        _context = context;
    }
    
    public async Task AddPersonAsync<TPerson>(
        TPerson person, CancellationToken cancellationToken)
        where TPerson : class, IPerson<IPersonId>
    {
        await _context
            .Set<TPerson>()
            .AddAsync(person, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}