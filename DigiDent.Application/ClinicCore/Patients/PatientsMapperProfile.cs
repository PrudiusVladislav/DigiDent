using AutoMapper;
using DigiDent.Application.ClinicCore.Patients.Queries.GetPatientById;
using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Visits;

namespace DigiDent.Application.ClinicCore.Patients;

public class PatientsMapperProfile: Profile
{
    public PatientsMapperProfile()
    {
        CreateMap<Appointment, AppointmentDTO>();
        CreateMap<Patient, PatientDTO>()
            .ForMember(
                dest => dest.NearestAppointments,
                opt => opt.MapFrom(src => src.Appointments
                    .Where(a => a.VisitDateTime >= DateTime.Now)
                    .OrderBy(a => a.VisitDateTime)
                    .Take(5)
                    .ToList()));
    }
}