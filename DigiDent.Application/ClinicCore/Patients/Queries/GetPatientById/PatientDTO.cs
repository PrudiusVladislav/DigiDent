namespace DigiDent.Application.ClinicCore.Patients.Queries.GetPatientById;

public record PatientDTO(
    Guid Id,
    string FullName,
    string Email,
    string PhoneNumber, 
    string Gender, 
    IEnumerable<AppointmentDTO> NearestAppointments);