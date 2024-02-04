using DigiDent.ClinicManagement.Domain.Employees.Doctors;
using DigiDent.ClinicManagement.Domain.Patients;
using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.ClinicManagement.Domain.Visits.Abstractions;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Abstractions.Errors;
using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.Appointments.Commands.CreateAppointment;

public sealed class CreateAppointmentCommandHandler
    : ICommandHandler<CreateAppointmentCommand, Result<Guid>>
{
    private readonly IAppointmentsRepository _appointmentsRepository;
    private readonly IProvidedServicesRepository _providedServicesRepository;
    private readonly IDoctorsRepository _doctorsRepository;
    private readonly IPatientsRepository _patientsRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateAppointmentCommandHandler(
        IAppointmentsRepository appointmentsRepository,
        IProvidedServicesRepository providedServicesRepository,
        IDoctorsRepository doctorsRepository,
        IPatientsRepository patientsRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _appointmentsRepository = appointmentsRepository;
        _providedServicesRepository = providedServicesRepository;
        _doctorsRepository = doctorsRepository;
        _patientsRepository = patientsRepository;
        _dateTimeProvider = dateTimeProvider;
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
        
        if (!doctor.IsAvailableAt(
                command.DateTime.Value, _dateTimeProvider, command.Duration))
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