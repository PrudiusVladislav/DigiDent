﻿using System.Text.Json;
using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DigiDent.ClinicManagement.EFCorePersistence.Shared.Configurations;

/// <summary>
/// Contains ValueConverters that are used in multiple configurations in the ClinicCoreContext.
/// </summary>
public static class SharedConverters
{
    public static ValueConverter<Money, decimal> MoneyConverter =>
        new ValueConverter<Money, decimal>(
            money => money.Amount,
            value => new Money(value));
    
    public static ValueConverter<T, string> JsonSerializeConverter<T>() =>
        new ValueConverter<T, string>(
            se => JsonSerializer
                .Serialize(se, JsonSerializerOptions.Default),
            value => JsonSerializer
                .Deserialize<T>(value, JsonSerializerOptions.Default)!);
    
    public static ValueConverter<TimeDuration, TimeSpan> TimeDurationConverter =>
        new ValueConverter<TimeDuration, TimeSpan>(
            duration => duration.Duration,
            value => new TimeDuration(value));
    
    public static ValueConverter<VisitDateTime, DateTime> VisitDateTimeConverter =>
        new ValueConverter<VisitDateTime, DateTime>(
            dateTime => dateTime.Value,
            value => new VisitDateTime(value));
}