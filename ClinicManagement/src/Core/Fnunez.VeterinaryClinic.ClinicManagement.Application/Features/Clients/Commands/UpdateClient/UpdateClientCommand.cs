using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.UpdateClient;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.UpdateClient;

public record UpdateClientCommand(UpdateClientRequest UpdateClientRequest) : IRequest<UpdateClientResponse>;