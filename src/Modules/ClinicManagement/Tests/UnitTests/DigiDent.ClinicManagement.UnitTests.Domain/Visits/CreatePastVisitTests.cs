using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Enumerations;
using DigiDent.Domain.ClinicCoreContext.Visits.Events;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Shared.Domain.Abstractions;
using DigiDent.Domain.UnitTests.ClinicCore.Visits.Extensions;
using DigiDent.Domain.UnitTests.ClinicCore.Visits.TestUtils.Constants;
using DigiDent.Shared.UnitTests.Domain.Extensions;

namespace DigiDent.Domain.UnitTests.ClinicCore.Visits;

public class CreatePastVisitTests
{
    [Fact]
    public void Create_WithValidData_ShouldReturnVisitAndRaiseCreatedEvent()
    {
        // Arrange
        PatientId patientId = TypedId.New<PatientId>();
        EmployeeId doctorId = TypedId.New<EmployeeId>();
        VisitDateTime visitDate = new(DateTime.Now);
        Money visitPrice = AppointmentConstants.DefaultPricePaidWhenCompleted;
        VisitStatus visitStatus = VisitStatus.Completed;
        IReadOnlyList<string> visitProcedures = AppointmentConstants.DefaultProcedures;
        TreatmentPlanId? planId = null;
        
        // Act
        PastVisit pastVisit = PastVisit.Create(
            doctorId, patientId, visitDate, visitPrice, visitStatus, visitProcedures, planId);

        // Assert
        pastVisit.ShouldRaiseDomainEvent<PastVisitCreatedDomainEvent>();
        pastVisit.ShouldBeCreatedFrom(
            doctorId, patientId, visitDate, visitPrice, visitStatus, visitProcedures, planId);
    }
}