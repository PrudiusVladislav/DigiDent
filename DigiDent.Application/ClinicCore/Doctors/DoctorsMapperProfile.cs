using AutoMapper;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;

namespace DigiDent.Application.ClinicCore.Doctors;

public class DoctorsMapperProfile: Profile
{
    public DoctorsMapperProfile()
    {
        CreateMap<Doctor, Queries.GetAllDoctors.DoctorDTO>();
        CreateMap<Doctor, Queries.GetDoctorById.DoctorDTO>();
    }
}