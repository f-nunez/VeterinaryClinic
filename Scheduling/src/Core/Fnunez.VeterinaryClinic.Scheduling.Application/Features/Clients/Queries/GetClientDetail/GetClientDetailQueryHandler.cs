using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientDetail;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientDetail;

public class GetClientDetailQueryHandler
    : IRequestHandler<GetClientDetailQuery, GetClientDetailResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfwork;

    public GetClientDetailQueryHandler(IMapper mapper, IUnitOfWork unitOfwork)
    {
        _mapper = mapper;
        _unitOfwork = unitOfwork;
    }

    public async Task<GetClientDetailResponse> Handle(
        GetClientDetailQuery query,
        CancellationToken cancellationToken)
    {
        GetClientDetailRequest request = query.GetClientDetailRequest;
        var response = new GetClientDetailResponse(request.CorrelationId);
        var specification = new GetClientByIdSpecification(request.ClientId);

        var clientDetail = await _unitOfwork
            .ReadRepository<Client>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (clientDetail is null)
            throw new NotFoundException(nameof(clientDetail), request.ClientId);

        response.ClientDetail = _mapper.Map<ClientDetailDto>(clientDetail);

        return response;
    }
}
