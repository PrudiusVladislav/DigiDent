using AutoMapper;
using DigiDent.Application.ClinicCore.ProvidedServices.Queries.GetAllProvidedServices;
using DigiDent.Application.ClinicCore.ProvidedServices.Queries.GetProvidedServiceById;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
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

        CreateMap<Doctor, DoctorByProvidedServiceDTO>();
        CreateMap<ProvidedService, SpecificProvidedServiceDTO>()
            .ForMember(
                ps => ps.Name,
                opt => opt.MapFrom(src => src.Details.Name))
            .ForMember(
                ps => ps.Description,
                opt => opt.MapFrom(src => src.Details.Description)); 
    }
}