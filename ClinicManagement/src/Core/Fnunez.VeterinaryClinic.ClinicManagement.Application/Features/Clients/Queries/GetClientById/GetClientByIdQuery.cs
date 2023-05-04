using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientById;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientById;

public record GetClientByIdQuery(GetClientByIdRequest GetClientByIdRequest) : IRequest<GetClientByIdResponse>;