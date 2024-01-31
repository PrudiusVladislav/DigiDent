using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.Shared.Errors;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors.ValueObjects;
using DigiDent.Shared.Domain.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Doctors.Commands.Update;

public sealed class UpdateDoctorCommandHandler
    : ICommandHandler<UpdateDoctorCommand, Result>
{
    private readonly IDoctorsRepository _doctorsRepository;

    public UpdateDoctorCommandHandler(IDoctorsRepository doctorsRepository)
    {
        _doctorsRepository = doctorsRepository;
    }

    public async Task<Result> Handle(
        UpdateDoctorCommand command, CancellationToken cancellationToken)
    {
        Doctor? doctor = await _doctorsRepository.GetByIdAsync(
            command.DoctorId, cancellationToken: cancellationToken);
        
        if (doctor is null) 
            return Result.Fail(RepositoryErrors
                .EntityNotFound<Doctor>(command.DoctorId.Value));
        
        UpdateDoctorDTO updateDoctorDTO = new(
            command.Gender,
            command.BirthDate,
            command.Status,
            command.Specialization,
            command.Biography);
        
        Result updateResult = doctor.Update(updateDoctorDTO);
        if (updateResult.IsFailure) 
            return updateResult;
        
        await _doctorsRepository.UpdateAsync(doctor, cancellationToken);
        return Result.Ok();
    }
}