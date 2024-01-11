using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Doctors.ValueObjects;

public record DoctorId(Guid Value): TypedId<Guid>(Value);