using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.ProvidedServices.Commands.AddService;

public sealed record AddProvidedServiceCommand : 
    ICommand<Result<Guid>>
{
    public ProvidedServiceDetails Details { get; init; } = null!;
    public TimeDuration UsualDuration { get; init; } = null!;
    public Money Price { get; init; } = null!;
    
    public static Result<AddProvidedServiceCommand> CreateFromRequest(
        AddProvidedServiceRequest request)
    {
        Result<ProvidedServiceDetails> detailsResult = ProvidedServiceDetails.Create(
            request.Name, request.Description);

        var usualDuration = TimeSpan.FromMinutes(request.UsualDuration);
        Result<TimeDuration> usualDurationResult = TimeDuration.Create(usualDuration);
        
        Result<Money> priceResult = Money.Create(request.Price);
        
        var mergedResult = Result.Merge(
            detailsResult, usualDurationResult, priceResult);

        if (mergedResult.IsFailure)
            return mergedResult.MapToType<AddProvidedServiceCommand>();
        
        return Result.Ok(new AddProvidedServiceCommand
        {
            Details = detailsResult.Value!,
            UsualDuration = usualDurationResult.Value!,
            Price = priceResult.Value!
        });
    }
}