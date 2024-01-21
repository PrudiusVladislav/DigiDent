using DigiDent.Application.ClinicCore.ProvidedServices.Queries.GetAllProvidedServices;

namespace DigiDent.Application.ClinicCore.ProvidedServices.Queries.GetProvidedServiceById;

public sealed class SpecificProvidedServiceDTO: ProvidedServiceDTO
{
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public IEnumerable<DoctorByProvidedServiceDTO> Doctors { get; set; }
        = Enumerable.Empty<DoctorByProvidedServiceDTO>();
}