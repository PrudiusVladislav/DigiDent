using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.ProvidedServices.Commands.AddService;

public class AddProvidedServiceCommandHandler
    : ICommandHandler<AddProvidedServiceCommand, Result<Guid>>
{
    private readonly IProvidedServicesRepository _providedServicesRepository;

    public AddProvidedServiceCommandHandler(IProvidedServicesRepository providedServicesRepository)
    {
        _providedServicesRepository = providedServicesRepository;
    }

    public async Task<Result<Guid>> Handle(
        AddProvidedServiceCommand request, CancellationToken cancellationToken)
    {
        var providedService = ProvidedService.Create(
            request.Details,
            request.UsualDuration,
            request.Price);
        
        await _providedServicesRepository.AddAsync(
            providedService, cancellationToken);
        
        return Result.Ok(providedService.Id.Value);
    }
}