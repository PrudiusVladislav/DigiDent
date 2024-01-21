using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.Doctors.Commands.Update;

public record UpdateDoctorCommand : ICommand<Result>
{
    public EmployeeId DoctorId { get; init; } = null!;
    public Gender? Gender { get; init; }
    public DateOnly? BirthDate { get; init; }
    public EmployeeStatus? Status { get; init; }
    public DoctorSpecialization? Specialization { get; init; }
    public string? Biography { get; init; }

    public static UpdateDoctorCommand CreateFromRequest(
        Guid id, UpdateDoctorRequest request)
    {
        var doctorId = new EmployeeId(id);
        
        Gender? parsedGender = Enum.TryParse<Gender>(
            request.Gender, true, out var gender) ? gender : null;
        
        EmployeeStatus? parsedStatus = Enum.TryParse<EmployeeStatus>(
            request.Status, true, out var status) ? status : null;
        
        DoctorSpecialization? parsedSpecialization = Enum.TryParse<DoctorSpecialization>(
            request.Specialization, true, out var specialization) ? specialization : null;
        
        return new UpdateDoctorCommand
        {
            DoctorId = doctorId,
            Gender = parsedGender,
            BirthDate = request.BirthDate,
            Status = parsedStatus,
            Specialization = parsedSpecialization,
            Biography = request.Biography
        };
    }
};