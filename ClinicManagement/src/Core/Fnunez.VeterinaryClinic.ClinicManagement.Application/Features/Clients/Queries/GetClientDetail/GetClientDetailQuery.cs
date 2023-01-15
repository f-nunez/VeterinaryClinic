using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientDetail;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientDetail;

public record GetClientDetailQuery(GetClientDetailRequest GetClientDetailRequest)
    : IRequest<GetClientDetailResponse>;