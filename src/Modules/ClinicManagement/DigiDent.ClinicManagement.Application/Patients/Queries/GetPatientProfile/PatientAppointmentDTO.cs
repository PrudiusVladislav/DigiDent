namespace DigiDent.ClinicManagement.Application.Patients.Queries.GetPatientProfile;

public class PatientAppointmentDTO
{
    public Guid Id { get; init; }
    public Guid DoctorId { get; init; }
    public string DoctorFullName { get; init; } = string.Empty;
    public DateTime VisitDateTime { get; init; }   
}
