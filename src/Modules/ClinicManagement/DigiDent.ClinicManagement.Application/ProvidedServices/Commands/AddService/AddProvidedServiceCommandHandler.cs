using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.ClinicManagement.Domain.Visits.Abstractions;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.ProvidedServices.Commands.AddService;

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