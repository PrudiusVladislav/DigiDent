using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.ClinicManagement.Domain.Patients.ValueObjects;
using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.ClinicManagement.Domain.Visits.Events;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using DigiDent.ClinicManagement.UnitTests.Domain.Visits.Extensions;
using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.UnitTests.Domain.Extensions;

namespace DigiDent.ClinicManagement.UnitTests.Domain.Visits;

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