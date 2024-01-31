using DigiDent.ClinicManagement.Domain.Visits;
using DigiDent.ClinicManagement.Domain.Visits.Abstractions;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Abstractions.Errors;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Application.ProvidedServices.Commands.UpdateService;

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