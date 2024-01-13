﻿using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;

public record WorkingDayId(Guid Value): TypedId<Guid>(Value);