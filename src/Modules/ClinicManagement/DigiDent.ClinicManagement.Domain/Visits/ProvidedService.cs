﻿using DigiDent.ClinicManagement.Domain.Employees.Doctors;
using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;
using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.ClinicManagement.Domain.Visits;

public class ProvidedService: IEntity<ProvidedServiceId, Guid>
{
    public ProvidedServiceId Id { get; init; }
    public ProvidedServiceDetails Details { get; private set; }
    public TimeDuration UsualDuration { get; private set; }
    public Money Price { get; private set; }
    
    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    
    // for EF Core
    internal ProvidedService() { }
    
    internal ProvidedService(
        ProvidedServiceId id,
        ProvidedServiceDetails details,
        TimeDuration usualDuration,
        Money price)
    {
        Id = id;
        Details = details;
        UsualDuration = usualDuration;
        Price = price;
    }
    
    public static ProvidedService Create(
        ProvidedServiceDetails details,
        TimeDuration usualDuration,
        Money price)
    {
        var serviceId = TypedId.New<ProvidedServiceId>();
        return new ProvidedService(
            serviceId,
            details,
            usualDuration,
            price);
    }

    public void Update(
        TimeDuration? usualDuration = null,
        Money? price = null)
    {
        UsualDuration = usualDuration ?? UsualDuration;
        Price = price ?? Price;
    }
}