using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.UserAccess.Commands.Shared;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.UserAccess.Commands.Refresh;

public record RefreshCommand(string AccessToken, string RefreshToken) 
    : ICommand<Result<AuthenticationResponse>>;