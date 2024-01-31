using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.Shared.Errors;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Shared.Domain.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Doctors.Commands.Update;

public sealed record UpdateDoctorCommand : ICommand<Result>
{
    public EmployeeId DoctorId { get; init; } = null!;
    public Gender? Gender { get; init; }
    public DateOnly? BirthDate { get; init; }
    public EmployeeStatus? Status { get; init; }
    public DoctorSpecialization? Specialization { get; init; }
    public string? Biography { get; init; }

    public static Result<UpdateDoctorCommand> CreateFromRequest(
        UpdateDoctorRequest request, Guid doctorToUpdateId)
    {
        EmployeeId doctorId = new(doctorToUpdateId);

        Result<Gender> genderResult = ParseEnum<Gender>(request.Gender);
        Result<EmployeeStatus> statusResult = ParseEnum<EmployeeStatus>(request.Status);
        Result<DoctorSpecialization> specializationResult = ParseEnum<DoctorSpecialization>(
            request.Specialization);
        
        var mergedResult = Result.Merge(
            genderResult, statusResult, specializationResult);

        if (mergedResult.IsFailure) 
            return mergedResult.MapToType<UpdateDoctorCommand>();
        
        return Result.Ok(new UpdateDoctorCommand
        {
            DoctorId = doctorId,
            Gender = genderResult.Value,
            BirthDate = request.BirthDate,
            Status = statusResult.Value,
            Specialization = specializationResult.Value,
            Biography = request.Biography
        });
    }
    
    private static Result<TEnum> ParseEnum<TEnum>(string? value)
        where TEnum : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Ok().MapToType<TEnum>();

        if (!Enum.TryParse<TEnum>(value, out var parsedValue))
        {
            return Result.Fail<TEnum>(CommandParametersErrors
                .IncorrectParameter<UpdateDoctorCommand>(typeof(TEnum).Name));
        }
        
        return Result.Ok(parsedValue);
    }
};