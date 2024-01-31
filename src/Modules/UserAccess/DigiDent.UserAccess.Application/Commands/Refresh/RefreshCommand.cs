using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.UserAccess.Commands.Shared;
using DigiDent.Shared.Domain.ReturnTypes;

namespace DigiDent.Application.UserAccess.Commands.Refresh;

public record RefreshCommand(string AccessToken, string RefreshToken) 
    : ICommand<Result<AuthenticationResponse>>;