using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.UserAccess.Application.Commands.Shared;

namespace DigiDent.UserAccess.Application.Commands.Refresh;

public record RefreshCommand(string AccessToken, string RefreshToken) 
    : ICommand<Result<AuthenticationResponse>>;