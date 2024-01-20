namespace DigiDent.Application.ClinicCore.Patients.Queries.GetPatientById;

public record PatientProfileDTO(
    Guid Id,
    string FullName,
    string Email,
    string PhoneNumber, 
    string Gender, 
    IEnumerable<NearestAppointmentDTO> NearestAppointments);