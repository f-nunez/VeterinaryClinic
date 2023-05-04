using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientDetail;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientDetail;

public record GetClientDetailQuery(GetClientDetailRequest GetClientDetailRequest)
    : IRequest<GetClientDetailResponse>;