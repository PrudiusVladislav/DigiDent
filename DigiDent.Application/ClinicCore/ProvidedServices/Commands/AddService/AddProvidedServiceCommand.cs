using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.ProvidedServices.Commands.AddService;

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