using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Enumerations;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using FluentAssertions;

namespace DigiDent.Domain.UnitTests.ClinicCore.Visits.Extensions;

public static class VisitValidationExtensions
{
    public static void ShouldBeCreatedFrom(
        this Appointment appointment,
        EmployeeId doctorId,
        PatientId patientId,
        VisitDateTime dateTime,
        TimeDuration duration,
        IEnumerable<ProvidedService> providedServices)
    {
        appointment.Should().NotBeNull();
        appointment.DoctorId.Should().Be(doctorId);
        appointment.PatientId.Should().Be(patientId);
        appointment.VisitDateTime.Should().Be(dateTime);
        appointment.Duration.Should().Be(duration);
        appointment.ProvidedServices.Should().BeEquivalentTo(providedServices);
    }

    public static void ShouldBeCreatedFrom(
        this PastVisit pastVisit,
        EmployeeId doctorId,
        PatientId patientId,
        VisitDateTime dateTime,
        Money pricePaid,
        VisitStatus status,
        IEnumerable<string> proceduresDone,
        TreatmentPlanId? treatmentPlanId)
    {
        pastVisit.Should().NotBeNull();
        pastVisit.DoctorId.Should().Be(doctorId);
        pastVisit.PatientId.Should().Be(patientId);
        pastVisit.VisitDateTime.Should().Be(dateTime);
        pastVisit.PricePaid.Should().Be(pricePaid);
        pastVisit.Status.Should().Be(status);
        pastVisit.ProceduresDone.Should().BeEquivalentTo(proceduresDone);
        pastVisit.TreatmentPlanId.Should().Be(treatmentPlanId);
    }
}