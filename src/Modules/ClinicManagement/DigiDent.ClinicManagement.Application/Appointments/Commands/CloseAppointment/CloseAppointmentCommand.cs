using DigiDent.ClinicManagement.Domain.Visits.Enumerations;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Abstractions.Errors;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.Appointments.Commands.CloseAppointment;

public sealed record CloseAppointmentCommand: ICommand<Result>
{
    public AppointmentId AppointmentId { get; init; } = null!;
    public Money Price { get; init; } = null!;
    public VisitStatus ClosureStatus { get; init; }
    
    public static Result<CloseAppointmentCommand> CreateFromRequest(
        Guid appointmentId, CloseAppointmentRequest request)
    {
        Result<Money> moneyResult = Money.Create(request.Price);
        Result<VisitStatus> statusResult = ParseVisitStatus(request.Status);
        
        Result mergedResult = Result.Merge(moneyResult, statusResult);
        if (mergedResult.IsFailure)
            return mergedResult.MapToType<CloseAppointmentCommand>();

        return Result.Ok(new CloseAppointmentCommand
        {
            AppointmentId = new AppointmentId(appointmentId),
            Price = moneyResult.Value!,
            ClosureStatus = statusResult.Value
        });
    }

    private static Result<VisitStatus> ParseVisitStatus(string value)
    {
        if (!Enum.TryParse<VisitStatus>(value, out var parsedValue))
        {
            return Result.Fail<VisitStatus>(CommandParametersErrors
                .IncorrectParameter<CloseAppointmentCommand>(nameof(VisitStatus)));
        }
        return Result.Ok(parsedValue);
    }
}