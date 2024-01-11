using DigiDent.Domain.SharedKernel;

namespace DigiDent.Domain.VisitsScheduleContext.Visits.ValueObjects.Ids;

public record VisitId(Guid Value): TypedId<Guid>(Value);