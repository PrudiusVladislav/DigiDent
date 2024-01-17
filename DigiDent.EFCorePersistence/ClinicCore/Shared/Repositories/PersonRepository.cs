
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.EFCorePersistence.ClinicCore.Shared.Repositories;

public class PersonRepository
    : IPersonRepository
{
    private readonly ClinicCoreDbContext _context;

    public PersonRepository(ClinicCoreDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync<TPerson, TPersonId>(TPerson person)
        where TPerson : class, IPerson<TPersonId>
        where TPersonId : IPersonId
    {
        await _context
            .Set<TPerson>()
            .AddAsync(person);
        await _context.SaveChangesAsync();
    }
}