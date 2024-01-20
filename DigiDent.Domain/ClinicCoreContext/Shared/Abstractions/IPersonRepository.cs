namespace DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;

public interface IPersonRepository
{
     Task AddPersonAsync<TPerson>(TPerson person, CancellationToken cancellationToken)
         where TPerson : class, IPerson<IPersonId>;
}