namespace DigiDent.ClinicManagement.Application.Patients.Queries.GetPatientProfile;

public class PatientProfileDTO
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Gender { get; init; } = string.Empty;
    
    public IEnumerable<PatientAppointmentDTO> Appointments { get; init; } 
        = Enumerable.Empty<PatientAppointmentDTO>();
    public IEnumerable<PatientTreatmentPlanDTO> TreatmentPlans { get; init; } 
        = Enumerable.Empty<PatientTreatmentPlanDTO>();
    public IEnumerable<PatientPastVisitDTO> PastVisits { get; init; } 
        = Enumerable.Empty<PatientPastVisitDTO>();
}