using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.ClinicManagement.Domain.Patients.ValueObjects;
using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.ClinicManagement.Domain.Visits.Enumerations;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using FluentAssertions;

namespace DigiDent.ClinicManagement.UnitTests.Domain.Visits.Extensions;

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