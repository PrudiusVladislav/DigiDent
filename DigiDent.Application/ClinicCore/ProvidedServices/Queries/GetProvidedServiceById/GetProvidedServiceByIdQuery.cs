﻿using DigiDent.Application.Shared.Abstractions;

namespace DigiDent.Application.ClinicCore.ProvidedServices.Queries.GetProvidedServiceById;

public record GetProvidedServiceByIdQuery(
    Guid Id) : IQuery<SpecificProvidedServiceDTO?>;