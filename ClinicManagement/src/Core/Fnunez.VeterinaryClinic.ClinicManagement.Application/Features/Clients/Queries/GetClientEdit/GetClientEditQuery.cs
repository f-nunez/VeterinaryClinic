using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientEdit;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientEdit;

public record GetClientEditQuery(GetClientEditRequest GetClientEditRequest)
    : IRequest<GetClientEditResponse>;