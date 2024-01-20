namespace DigiDent.Application.ClinicCore.Patients.Queries.GetPatientById;

public record NearestAppointmentDTO(
    Guid Id,
    Guid DoctorId,
    string DoctorFullName,
    DateTime VisitDateTime);
