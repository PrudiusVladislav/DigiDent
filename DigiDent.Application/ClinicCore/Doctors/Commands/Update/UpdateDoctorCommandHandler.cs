using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.Shared.Errors;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors.ValueObjects;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Doctors.Commands.Update;

public class UpdateDoctorCommandHandler
    : ICommandHandler<UpdateDoctorCommand, Result>
{
    private readonly IDoctorsRepository _doctorsRepository;

    public UpdateDoctorCommandHandler(IDoctorsRepository doctorsRepository)
    {
        _doctorsRepository = doctorsRepository;
    }

    public async Task<Result> Handle(
        UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorsRepository.GetByIdAsync(
            request.DoctorId, cancellationToken: cancellationToken);
        
        if (doctor is null) 
            return Result.Fail(RepositoryErrors
                .EntityNotFound<Doctor>(request.DoctorId.Value));
        
        var updateDoctorDTO = new UpdateDoctorDTO(
            request.Gender,
            request.BirthDate,
            request.Status,
            request.Specialization,
            request.Biography);
        
        var updateResult = doctor.Update(updateDoctorDTO);
        if (updateResult.IsFailure) return updateResult;
        
        await _doctorsRepository.UpdateAsync(doctor, cancellationToken);
        return Result.Ok();
    }
}