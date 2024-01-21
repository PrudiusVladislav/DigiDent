using AutoMapper;
using DigiDent.Application.ClinicCore.ProvidedServices.Queries.GetAllProvidedServices;
using DigiDent.Domain.ClinicCoreContext.Visits;

namespace DigiDent.Application.ClinicCore.ProvidedServices.Queries;

public sealed class ProvidedServicesMapperProfile: Profile
{
    public ProvidedServicesMapperProfile()
    {
        CreateMap<ProvidedService, ProvidedServiceDTO>()
            .ForMember(
                ps => ps.Name,
                opt => opt.MapFrom(src => src.Details.Name));
    }
}