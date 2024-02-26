using DigiDent.ClinicManagement.Domain.Employees.Doctors;
using DigiDent.ClinicManagement.Domain.Employees.Doctors.ValueObjects;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Abstractions.Errors;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.Doctors.Commands.Update;

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
                .EntityNotFound<Doctor, Guid>(command.DoctorId));
        
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