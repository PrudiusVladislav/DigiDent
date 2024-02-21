using DigiDent.ClinicManagement.Domain.Employees.Doctors.ValueObjects;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Abstractions.Errors;
using DigiDent.Shared.Kernel.Extensions;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.Doctors.Commands.Update;

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

        return value.ToEnum<TEnum>();
    }
};