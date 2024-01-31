using AutoMapper;
using DigiDent.ClinicManagement.Application.ProvidedServices.Queries.GetAllProvidedServices;
using DigiDent.ClinicManagement.Application.ProvidedServices.Queries.GetProvidedServiceById;
using DigiDent.ClinicManagement.Domain.Employees.Doctors;
using DigiDent.ClinicManagement.Domain.Visits;

namespace DigiDent.ClinicManagement.Application.ProvidedServices.Queries;

public class ProvidedServicesMapperProfile: Profile
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