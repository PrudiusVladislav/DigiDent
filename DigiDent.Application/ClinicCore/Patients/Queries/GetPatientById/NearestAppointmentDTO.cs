namespace DigiDent.Application.ClinicCore.Patients.Queries.GetPatientById;

public class NearestAppointmentDTO
{
    public Guid Id { get; init; }
    public Guid DoctorId { get; init; }
    public string DoctorFullName { get; init; } = string.Empty;
    public DateTime VisitDateTime { get; init; }   
}
