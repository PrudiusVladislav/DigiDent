namespace DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;

public interface IPersonRepository
{
     Task AddAsync<TPerson, TPersonId>(TPerson person)
          where TPerson : class, IPerson<TPersonId>
          where TPersonId : IPersonId;
}