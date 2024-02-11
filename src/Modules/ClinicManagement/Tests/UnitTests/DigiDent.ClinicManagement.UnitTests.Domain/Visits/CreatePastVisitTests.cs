using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.ClinicManagement.Domain.Patients.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.ClinicManagement.Domain.Visits.Enumerations;
using DigiDent.ClinicManagement.Domain.Visits.Events;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;
using DigiDent.ClinicManagement.UnitTests.Domain.Visits.Extensions;
using DigiDent.ClinicManagement.UnitTests.Domain.Visits.TestUtils.Constants;
using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.UnitTests.Domain.Extensions;

namespace DigiDent.ClinicManagement.UnitTests.Domain.Visits;

public class CreatePastVisitTests
{
    [Fact]
    public void Create_WithValidData_ShouldReturnVisitAndRaiseCreatedEvent()
    {
        // Arrange
        PastVisitId id = TypedId.New<PastVisitId>();
        PatientId patientId = TypedId.New<PatientId>();
        EmployeeId doctorId = TypedId.New<EmployeeId>();
        VisitDateTime visitDate = new(DateTime.Now);
        Money visitPrice = AppointmentConstants.DefaultPricePaidWhenCompleted;
        VisitStatus visitStatus = VisitStatus.Completed;
        IReadOnlyList<string> visitProcedures = AppointmentConstants.DefaultProcedures;
        TreatmentPlanId? planId = null;
        
        // Act
        PastVisit pastVisit = new(
            id,
            doctorId,
            patientId,
            visitDate, 
            visitPrice, 
            visitStatus, 
            visitProcedures, 
            planId);

        // Assert
        pastVisit.ShouldRaiseDomainEvent<PastVisitCreatedDomainEvent>();
        pastVisit.ShouldBeCreatedFrom(
            id,
            doctorId,
            patientId,
            visitDate, 
            visitPrice,
            visitStatus,
            visitProcedures, 
            planId);
    }
}