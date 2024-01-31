using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;
using DigiDent.Shared.Domain.ReturnTypes;

namespace DigiDent.Application.ClinicCore.ProvidedServices.Commands.AddService;

public sealed class AddProvidedServiceCommandHandler
    : ICommandHandler<AddProvidedServiceCommand, Result<Guid>>
{
    private readonly IProvidedServicesRepository _providedServicesRepository;

    public AddProvidedServiceCommandHandler(
        IProvidedServicesRepository providedServicesRepository)
    {
        _providedServicesRepository = providedServicesRepository;
    }

    public async Task<Result<Guid>> Handle(
        AddProvidedServiceCommand command, CancellationToken cancellationToken)
    {
        ProvidedService providedService = ProvidedService.Create(
            command.Details,
            command.UsualDuration,
            command.Price);
        
        await _providedServicesRepository.AddAsync(
            providedService, cancellationToken);
        
        return Result.Ok(providedService.Id.Value);
    }
}