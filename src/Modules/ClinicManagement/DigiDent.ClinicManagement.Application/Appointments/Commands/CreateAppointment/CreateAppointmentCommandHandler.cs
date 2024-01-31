using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.Shared.Errors;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Appointments.Commands.CreateAppointment;

public sealed class CreateAppointmentCommandHandler
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
        CreateAppointmentCommand command, CancellationToken cancellationToken)
    {
        Patient? patient = await _patientsRepository.GetByIdAsync(
            command.PatientId, cancellationToken);
        
        if (patient is null)
            return Result.Fail<Guid>(RepositoryErrors
                .EntityNotFound<Patient>(command.PatientId.Value));
        
        Doctor? doctor = await _doctorsRepository.GetByIdAsync(
                command.DoctorId,
                includeScheduling: true,
                cancellationToken: cancellationToken);
        
        if (doctor is null)
            return Result.Fail<Guid>(RepositoryErrors
                .EntityNotFound<Doctor>(command.DoctorId.Value));
        
        if (!doctor.IsAvailableAt(command.DateTime.Value,
                currentDateTime: DateTime.Now, command.Duration))
        {
            return Result.Fail<Guid>(AppointmentCreationErrors
                .DoctorIsNotAvailable);
        }
        
        var services = await _providedServicesRepository
            .GetAllFromIdsAsync(command.ServicesIds, cancellationToken);
        
        if (services.Count == 0)
            return Result.Fail<Guid>(AppointmentCreationErrors
                .NoServicesProvided);
        
        Appointment appointment = Appointment.Create(
            command.DoctorId,
            command.PatientId,
            command.DateTime,
            command.Duration,
            services);
        
        await _appointmentsRepository.AddAsync(appointment, cancellationToken);
        
        return Result.Ok(appointment.Id.Value);
    }
}