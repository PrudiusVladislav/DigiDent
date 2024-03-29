﻿using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;

namespace DigiDent.ClinicManagement.UnitTests.Domain.Visits.TestUtils.Constants;

public class AppointmentConstants
{
    public static readonly TimeSpan DefaultDuration = TimeSpan.FromMinutes(30);
    public static readonly TimeSpan DefaultVisitDateTimeOffset = TimeSpan.FromHours(1);
    
    public static readonly Money DefaultPricePaidWhenCompleted = new(100);
    public static readonly IReadOnlyList<string> DefaultProcedures 
        = new List<string> { "Procedure 1", "Procedure 2" };
}