﻿using DigiDent.ClinicManagement.Application;
using DigiDent.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.ClinicManagement.API;

/// <summary>
/// Marker and loader class for the ClinicManagement module
/// </summary>
public sealed class ClinicManagementModule: IModule
{
    public string Name => nameof(ClinicManagementModule);

    public void RegisterDependencies(IServiceCollection services, IConfiguration configuration,
        MediatRServiceConfiguration mediatrConfiguration)
    {
        throw new NotImplementedException();
    }

    public void RegisterEndpoints(IEndpointRouteBuilder builder)
    {
        throw new NotImplementedException();
    }
}