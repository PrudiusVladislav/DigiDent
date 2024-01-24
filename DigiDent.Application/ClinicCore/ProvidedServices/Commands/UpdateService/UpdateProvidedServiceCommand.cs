using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.ProvidedServices.Commands.UpdateService;

public record UpdateProvidedServiceCommand: ICommand<Result>
{
    public ProvidedServiceId Id { get; init; } = null!;
    public TimeDuration? UsualDuration { get; init; }
    public Money? Price { get; init; }
    
    public static Result<UpdateProvidedServiceCommand> CreateFromRequest(
        Guid id, UpdateProvidedServiceRequest request)
    {
        var serviceId = new ProvidedServiceId(id);
        var durationResult = request.Duration is null
            ? Result.Ok().MapToType<TimeDuration>()
            : TimeDuration.Create(TimeSpan
                .FromMinutes(request.Duration.Value));
        
        var priceResult = request.Price is null
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