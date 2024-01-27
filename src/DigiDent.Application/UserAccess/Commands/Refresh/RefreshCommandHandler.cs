using System.Security.Claims;
using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.UserAccess.Abstractions;
using DigiDent.Application.UserAccess.Commands.Shared;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;

namespace DigiDent.Application.UserAccess.Commands.Refresh;

public class RefreshCommandHandler
    : ICommandHandler<RefreshCommand, Result<AuthenticationResponse>>
{
    private readonly IJwtService _jwtService;
    private readonly IRefreshTokensRepository _refreshTokensRepository;
    private readonly IUsersRepository _usersRepository;
    
    public RefreshCommandHandler(
        IJwtService jwtService,
        IRefreshTokensRepository refreshTokensRepository,
        IUsersRepository usersRepository)
    {
        _jwtService = jwtService;
        _refreshTokensRepository = refreshTokensRepository;
        _usersRepository = usersRepository;
    }
    
    public async Task<Result<AuthenticationResponse>> Handle(
        RefreshCommand request,
        CancellationToken cancellationToken)
    {
        
        Result<ClaimsPrincipal> refreshRequestValidationResult = await _jwtService
            .ValidateRefreshRequestAsync(
                request.AccessToken,
                request.RefreshToken,
                cancellationToken);
        
        if (refreshRequestValidationResult.IsFailure)
            return refreshRequestValidationResult.MapToType<AuthenticationResponse>();
        
        await _refreshTokensRepository.DeleteRefreshTokenAsync(
            request.RefreshToken, cancellationToken);

        string rawUserId = refreshRequestValidationResult.Value!
            .Claims.Single(x => x.Type == ClaimTypes.NameIdentifier)
            .Value;
        UserId userId = new UserId(Guid.Parse(rawUserId));
        
        User user = (await _usersRepository.GetByIdAsync(userId, cancellationToken))!;
        return Result.Ok(await _jwtService.GenerateAuthenticationResponseAsync(
            user, cancellationToken));
    }
}