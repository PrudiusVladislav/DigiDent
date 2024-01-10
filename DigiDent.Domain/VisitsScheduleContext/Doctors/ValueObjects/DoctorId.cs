using DigiDent.Domain.SharedKernel;

namespace DigiDent.Domain.VisitsScheduleContext.Doctors.ValueObjects;

public record DoctorId(Guid Value): TypedId<Guid>(Value);