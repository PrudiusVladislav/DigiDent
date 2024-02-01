using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.ClinicManagement.Domain.Patients.ValueObjects;
using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.ClinicManagement.Domain.Visits.Enumerations;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;
using DigiDent.ClinicManagement.UnitTests.Domain.Visits.TestUtils.Constants;
using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.ClinicManagement.UnitTests.Domain.Visits.TestUtils;

public class AppointmentFactory
{
    public static Appointment CreateValidAppointment()
    {
        AppointmentId appointmentId = TypedId.New<AppointmentId>();
        EmployeeId doctorId = TypedId.New<EmployeeId>();
        PatientId patientId = TypedId.New<PatientId>();
        
        //By default, the visit is scheduled in the future
        VisitDateTime visitDateTime = new(
            DateTime.Now.Add(AppointmentConstants.DefaultVisitDateTimeOffset));
        
        TimeDuration duration = new(AppointmentConstants.DefaultDuration);
        
        AppointmentStatus status = AppointmentStatus.Scheduled;
        IEnumerable<ProvidedService> providedServices = new List<ProvidedService>();
        
        return new Appointment(
            appointmentId,
            doctorId,
            patientId,
            visitDateTime,
            duration, 
            status, 
            providedServices);
    }
}