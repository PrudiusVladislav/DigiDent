using AutoMapper;
using DigiDent.ClinicManagement.Application.Patients.Queries.GetAllPatients;
using DigiDent.ClinicManagement.Application.Patients.Queries.GetPatientProfile;
using DigiDent.ClinicManagement.Domain.Patients;
using DigiDent.ClinicManagement.Domain.Visits;

namespace DigiDent.ClinicManagement.Application.Patients;

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