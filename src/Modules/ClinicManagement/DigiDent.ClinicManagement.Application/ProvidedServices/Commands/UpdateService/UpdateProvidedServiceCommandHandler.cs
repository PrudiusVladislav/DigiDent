using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.Shared.Errors;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;
using DigiDent.Shared.Domain.ReturnTypes;

namespace DigiDent.Application.ClinicCore.ProvidedServices.Commands.UpdateService;

public sealed class UpdateProvidedServiceCommandHandler
    : ICommandHandler<UpdateProvidedServiceCommand, Result>
{
    private readonly IProvidedServicesRepository _providedServicesRepository;

    public UpdateProvidedServiceCommandHandler(
        IProvidedServicesRepository providedServicesRepository)
    {
        _providedServicesRepository = providedServicesRepository;
    }

    public async Task<Result> Handle(
        UpdateProvidedServiceCommand command, CancellationToken cancellationToken)
    {
        ProvidedService? providedService = await _providedServicesRepository.GetByIdAsync(
            command.Id, cancellationToken);
        
        if (providedService is null)
            return Result.Fail(RepositoryErrors
                .EntityNotFound<ProvidedService>(command.Id.Value));
        
        providedService.Update(command.UsualDuration, command.Price);
        await _providedServicesRepository.UpdateAsync(
            providedService, cancellationToken);
        
        return Result.Ok();
    }
}