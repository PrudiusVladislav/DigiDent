using AutoMapper;
using DigiDent.ClinicManagement.Application.Doctors.Queries.GetAllDoctors;
using DigiDent.ClinicManagement.Application.Doctors.Queries.GetDoctorById;
using DigiDent.ClinicManagement.Domain.Employees.Doctors;

namespace DigiDent.ClinicManagement.Application.Doctors.Queries;

public class DoctorsMapperProfile: Profile
{
    public DoctorsMapperProfile()
    {
        CreateMap<Doctor, DoctorDTO>();
        CreateMap<Doctor, DoctorProfileDTO>();
    }
}