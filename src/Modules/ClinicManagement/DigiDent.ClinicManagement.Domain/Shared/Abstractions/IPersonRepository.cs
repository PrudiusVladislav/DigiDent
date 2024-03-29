﻿namespace DigiDent.ClinicManagement.Domain.Shared.Abstractions;

public interface IPersonRepository
{
     Task AddPersonAsync<TPerson, TId>(TPerson person, CancellationToken cancellationToken)
         where TPerson : class, IPerson<TId>
         where TId : IPersonId;
}