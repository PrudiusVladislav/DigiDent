namespace DigiDent.Application.ClinicCore.Patients.Queries.GetPatientById;

public record AppointmentDTO(
    Guid Id,
    Guid DoctorId,
    string DoctorFullName,
    DateTime VisitDateTime);
