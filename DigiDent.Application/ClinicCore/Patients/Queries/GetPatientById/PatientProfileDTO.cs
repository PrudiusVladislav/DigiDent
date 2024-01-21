namespace DigiDent.Application.ClinicCore.Patients.Queries.GetPatientById;

public class PatientProfileDTO
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Gender { get; init; } = string.Empty;
    public IEnumerable<NearestAppointmentDTO> NearestAppointments { get; init; } 
        = Enumerable.Empty<NearestAppointmentDTO>();
}