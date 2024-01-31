using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Events;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Shared.Domain.Abstractions;
using DigiDent.Domain.UnitTests.ClinicCore.Visits.Extensions;
using DigiDent.Domain.UnitTests.Shared;

namespace DigiDent.Domain.UnitTests.ClinicCore.Visits;

public class CreateAppointmentTests
{
    [Fact]
    public void Create_WithValidData_ShouldReturnCreatedAndRaiseEvent()
    {
        // Arrange
        EmployeeId doctorId = TypedId.New<EmployeeId>();
        PatientId patientId = TypedId.New<PatientId>();
        VisitDateTime visitDateTime = new(DateTime.Now.AddDays(1));
        TimeDuration duration = new(TimeSpan.FromHours(1));
        IEnumerable<ProvidedService> providedServices = new List<ProvidedService>();
        
        // Act
        var appointment = Appointment.Create(
            doctorId, patientId, visitDateTime, duration, providedServices);
        
        // Assert
        appointment.ShouldBeCreatedFrom(
            doctorId, patientId, visitDateTime, duration, providedServices);
        
        appointment.ShouldRaiseDomainEvent<AppointmentCreatedDomainEvent>();
    }
}