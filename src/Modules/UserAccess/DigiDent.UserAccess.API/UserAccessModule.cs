﻿using DigiDent.Shared.Infrastructure.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.UserAccess.API;

/// <summary>
/// Marker and loader class for the UserAccess module
/// </summary>
public sealed class UserAccessModule: IModule
{
    public string Name => nameof(UserAccessModule);
    
    public void Register(IServiceCollection services)
    {
        throw new NotImplementedException();
    }
}