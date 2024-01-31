using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.ClinicManagement.Domain.Visits.Enumerations;
using DigiDent.ClinicManagement.Domain.Visits.Events;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using DigiDent.ClinicManagement.UnitTests.Domain.Visits.TestUtils;
using DigiDent.ClinicManagement.UnitTests.Domain.Visits.TestUtils.Constants;
using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.Shared.UnitTests.Domain.Extensions;
using FluentAssertions;
using NSubstitute;

namespace DigiDent.ClinicManagement.UnitTests.Domain.Visits;

public class CloseAppointmentTests
{
    [Theory]
    [MemberData(nameof(CreateValidAppointmentsClosureArguments))]
    public void Close_WhenParametersValid_ShouldRaiseAppointmentCloseEvent(
        VisitStatus visitStatus, Money pricePaid, TimeSpan visitDateTimeOffset)
    {
        // Arrange
        Appointment appointment = AppointmentFactory.CreateValidAppointment();
        IDateTimeProvider dateTimeProvider = Substitute.For<IDateTimeProvider>();
        
        dateTimeProvider.Now.Returns(
            appointment.VisitDateTime.Value.Add(visitDateTimeOffset));

        // Act
        Result closureResult = appointment.Close(
            visitStatus,
            pricePaid,
            dateTimeProvider);

        // Assert
        closureResult.IsSuccess.Should().BeTrue();
        appointment.ShouldRaiseDomainEvent<AppointmentClosedDomainEvent>();
    }
    
    [Theory]
    [MemberData(nameof(CreateInvalidAppointmentsClosureArguments))]
    public void Close_WhenParametersAreInvalid_ShouldFailAndNotRaiseCloseEvent(
        VisitStatus visitStatus, Money pricePaid, TimeSpan visitDateTimeOffset)
    {
        // Arrange
        Appointment appointment = AppointmentFactory.CreateValidAppointment();
        IDateTimeProvider dateTimeProvider = Substitute.For<IDateTimeProvider>();
        
        dateTimeProvider.Now.Returns(
            appointment.VisitDateTime.Value.Add(visitDateTimeOffset));

        // Act
        Result closureResult = appointment.Close(
            visitStatus,
            pricePaid,
            dateTimeProvider);

        // Assert
        closureResult.IsFailure.Should().BeTrue();
        appointment.ShouldNotRaiseDomainEvent<AppointmentClosedDomainEvent>();
    }
    
    public static IEnumerable<object[]> CreateValidAppointmentsClosureArguments()
    {
        yield return
        [
            VisitStatus.Completed,
            AppointmentConstants.DefaultPricePaidWhenCompleted,
            AppointmentConstants.DefaultVisitDateTimeOffset
        ];
        yield return
        [
            VisitStatus.Missed,
            Money.Zero,
            AppointmentConstants.DefaultVisitDateTimeOffset
        ];
        yield return 
        [
            VisitStatus.Canceled,
            Money.Zero,
            AppointmentConstants.DefaultVisitDateTimeOffset.Negate()
        ];
    }
    
    public static IEnumerable<object[]> CreateInvalidAppointmentsClosureArguments()
    {
        yield return
        [
            VisitStatus.Completed,
            Money.Zero,
            AppointmentConstants.DefaultVisitDateTimeOffset
        ];
        yield return
        [
            VisitStatus.Missed,
            AppointmentConstants.DefaultPricePaidWhenCompleted,
            AppointmentConstants.DefaultVisitDateTimeOffset
        ];
        yield return
        [
            VisitStatus.Canceled,
            AppointmentConstants.DefaultPricePaidWhenCompleted,
            AppointmentConstants.DefaultVisitDateTimeOffset
        ];
        yield return 
        [
            VisitStatus.Canceled,
            AppointmentConstants.DefaultPricePaidWhenCompleted,
            AppointmentConstants.DefaultVisitDateTimeOffset
        ];
        yield return 
        [
            VisitStatus.Missed,
            Money.Zero,
            AppointmentConstants.DefaultVisitDateTimeOffset.Negate()
        ];
        yield return 
        [
            VisitStatus.Completed,
            AppointmentConstants.DefaultPricePaidWhenCompleted,
            AppointmentConstants.DefaultVisitDateTimeOffset.Negate()
        ];
    }
}