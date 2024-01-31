
using DigiDent.ClinicManagement.Domain.Shared.Abstractions;

namespace DigiDent.ClinicManagement.EFCorePersistence.Shared.Repositories;

public class PersonRepository: IPersonRepository
{
    private readonly ClinicCoreDbContext _context;

    public PersonRepository(ClinicCoreDbContext context)
    {
        _context = context;
    }
    
    public async Task AddPersonAsync<TPerson, TId>(
        TPerson person, CancellationToken cancellationToken)
        where TPerson : class, IPerson<TId>
        where TId : IPersonId
    {
        await _context
            .Set<TPerson>()
            .AddAsync(person, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}