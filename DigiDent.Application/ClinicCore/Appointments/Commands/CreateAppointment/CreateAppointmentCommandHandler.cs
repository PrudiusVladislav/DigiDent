using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.Shared.Errors;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Appointments.Commands.CreateAppointment;

public class CreateAppointmentCommandHandler
    : ICommandHandler<CreateAppointmentCommand, Result<Guid>>
{
    private readonly IAppointmentsRepository _appointmentsRepository;
    private readonly IProvidedServicesRepository _providedServicesRepository;
    private readonly IDoctorsRepository _doctorsRepository;
    private readonly IPatientsRepository _patientsRepository;

    public CreateAppointmentCommandHandler(
        IAppointmentsRepository appointmentsRepository,
        IProvidedServicesRepository providedServicesRepository,
        IDoctorsRepository doctorsRepository,
        IPatientsRepository patientsRepository)
    {
        _appointmentsRepository = appointmentsRepository;
        _providedServicesRepository = providedServicesRepository;
        _doctorsRepository = doctorsRepository;
        _patientsRepository = patientsRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        if (request.DateTime <= DateTime.Now)
            return Result.Fail<Guid>(AppointmentCreationErrors
                .AppointmentDateIsInThePast);
        
        Patient? patient = await _patientsRepository.GetByIdAsync(
            request.PatientId, cancellationToken);
        
        if (patient is null)
            return Result.Fail<Guid>(RepositoryErrors
                .EntityNotFound<Patient>(request.PatientId.Value));
        
        Doctor? doctor = await _doctorsRepository.GetByIdAsync(
                request.DoctorId,
                includeScheduling: true,
                cancellationToken: cancellationToken);
        
        if (doctor is null)
            return Result.Fail<Guid>(RepositoryErrors
                .EntityNotFound<Doctor>(request.DoctorId.Value));
        
        if (!doctor.IsAvailableAt(request.DateTime,
                currentDateTime: DateTime.Now, request.Duration))
        {
            return Result.Fail<Guid>(AppointmentCreationErrors
                .DoctorIsNotAvailable);
        }
        
        var services = await _providedServicesRepository
            .GetAllFromIdsAsync(request.ServicesIds, cancellationToken);
        
        if (services.Count == 0)
            return Result.Fail<Guid>(AppointmentCreationErrors
                .NoServicesProvided);
        
        var appointment = Appointment.Create(
            request.DoctorId,
            request.PatientId,
            request.DateTime,
            request.Duration,
            services);
        
        await _appointmentsRepository.AddAsync(appointment, cancellationToken);
        
        return Result.Ok(appointment.Id.Value);
    }
}