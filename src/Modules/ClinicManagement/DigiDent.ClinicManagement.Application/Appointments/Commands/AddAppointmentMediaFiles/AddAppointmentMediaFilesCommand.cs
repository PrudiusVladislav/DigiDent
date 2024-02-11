using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Kernel.ReturnTypes;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DigiDent.ClinicManagement.Application.Appointments.Commands.AddAppointmentMediaFiles;

public sealed record AddAppointmentMediaFilesCommand(
    Guid AppointmentId,
    List<IFormFile> MediaFiles) : ICommand<Result>;