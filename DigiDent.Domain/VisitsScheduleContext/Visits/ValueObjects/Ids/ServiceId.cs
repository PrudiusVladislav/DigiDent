using DigiDent.Domain.SharedKernel;

namespace DigiDent.Domain.VisitsScheduleContext.Visits.ValueObjects.Ids;

public record ServiceId(int Value): TypedId<int>(Value);