using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.DeleteClient;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.DeleteClient;

public record DeleteClientCommand(DeleteClientRequest DeleteClientRequest)
    : IRequest<DeleteClientResponse>;