using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.CreateClient;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.CreateClient;

public record CreateClientCommand(CreateClientRequest CreateClientRequest) : IRequest<CreateClientResponse>;