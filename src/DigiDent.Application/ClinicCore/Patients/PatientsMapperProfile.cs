using AutoMapper;
using DigiDent.Application.ClinicCore.Patients.Queries.GetAllPatients;
using DigiDent.Application.ClinicCore.Patients.Queries.GetPatientProfile;
using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Visits;

namespace DigiDent.Application.ClinicCore.Patients;

public class PatientsMapperProfile: Profile
{
    public PatientsMapperProfile()
    {
        CreateMap<Patient, PatientDTO>();
        
        
        CreateMap<TreatmentPlan, PatientTreatmentPlanDTO>()
            .ForMember(
                tp => tp.DiagnosisDescription,
                opt => opt.MapFrom(src => src.Details.DiagnosisDescription));
        CreateMap<Appointment, PatientAppointmentDTO>()
            .ForMember(
                a => a.DoctorFullName,
                opt => opt.MapFrom(src => src.Doctor.FullName));
        CreateMap<PastVisit, PatientPastVisitDTO>()
            .ForMember(
                pv => pv.DoctorFullName,
                opt => opt.MapFrom(src => src.Doctor.FullName));
        CreateMap<Patient, PatientProfileDTO>();
    }
}