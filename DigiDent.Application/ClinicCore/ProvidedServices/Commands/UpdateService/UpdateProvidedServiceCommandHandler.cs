using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.Shared.Errors;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.ClinicCore.ProvidedServices.Commands.UpdateService;

public class UpdateProvidedServiceCommandHandler
    : ICommandHandler<UpdateProvidedServiceCommand, Result>
{
    private readonly IProvidedServicesRepository _providedServicesRepository;

    public UpdateProvidedServiceCommandHandler(IProvidedServicesRepository providedServicesRepository)
    {
        _providedServicesRepository = providedServicesRepository;
    }

    public async Task<Result> Handle(
        UpdateProvidedServiceCommand request, CancellationToken cancellationToken)
    {
        var providedService = await _providedServicesRepository.GetByIdAsync(
            request.Id, cancellationToken: cancellationToken);
        
        if (providedService is null)
            return Result.Fail(RepositoryErrors
                .EntityNotFound<ProvidedService>(request.Id.Value));
        
        providedService.Update(request.UsualDuration, request.Price);
        await _providedServicesRepository.UpdateAsync(
            providedService, cancellationToken);
        
        return Result.Ok();
    }
}