using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.ProvidedServices.Commands.UpdateService;

public sealed record UpdateProvidedServiceCommand: ICommand<Result>
{
    public ProvidedServiceId Id { get; init; } = null!;
    public TimeDuration? UsualDuration { get; init; }
    public Money? Price { get; init; }
    
    public static Result<UpdateProvidedServiceCommand> CreateFromRequest(
        UpdateProvidedServiceRequest request, Guid serviceToUpdateId)
    {
        ProvidedServiceId serviceId = new(serviceToUpdateId);
        
        Result<TimeDuration> durationResult = request.Duration is null
            ? Result.Ok().MapToType<TimeDuration>()
            : TimeDuration.Create(TimeSpan
                .FromMinutes(request.Duration.Value));
        
        Result<Money> priceResult = request.Price is null
            ? Result.Ok().MapToType<Money>()
            : Money.Create(request.Price.Value);
        
        var mergedResult = Result.Merge(durationResult, priceResult);

        return mergedResult.Match(
            onFailure: _ => mergedResult.MapToType<UpdateProvidedServiceCommand>(),
            onSuccess: () => Result.Ok(new UpdateProvidedServiceCommand
            {
                Id = serviceId,
                UsualDuration = durationResult.Value,
                Price = priceResult.Value
            }));
    }
}