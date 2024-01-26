using AutoMapper;
using DigiDent.Application.ClinicCore.Doctors.Queries.GetAllDoctors;
using DigiDent.Application.ClinicCore.Doctors.Queries.GetDoctorById;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;

namespace DigiDent.Application.ClinicCore.Doctors.Queries;

public class DoctorsMapperProfile: Profile
{
    public DoctorsMapperProfile()
    {
        CreateMap<Doctor, DoctorDTO>();
        CreateMap<Doctor, DoctorProfileDTO>();
    }
}