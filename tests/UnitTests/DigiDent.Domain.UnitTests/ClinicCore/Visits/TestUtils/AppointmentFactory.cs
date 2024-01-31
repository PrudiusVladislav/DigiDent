using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Enumerations;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.UnitTests.ClinicCore.Visits.TestUtils.Constants;

namespace DigiDent.Domain.UnitTests.ClinicCore.Visits.TestUtils;

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