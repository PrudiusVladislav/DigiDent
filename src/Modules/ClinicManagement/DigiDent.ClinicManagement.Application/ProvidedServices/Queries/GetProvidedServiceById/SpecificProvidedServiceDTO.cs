﻿
namespace DigiDent.ClinicManagement.Application.ProvidedServices.Queries.GetProvidedServiceById;

public class SpecificProvidedServiceDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TimeSpan UsualDuration { get; init; }
    public decimal Price { get; set; }
    public IEnumerable<DoctorByProvidedServiceDTO> Doctors { get; set; }
        = Enumerable.Empty<DoctorByProvidedServiceDTO>();
}