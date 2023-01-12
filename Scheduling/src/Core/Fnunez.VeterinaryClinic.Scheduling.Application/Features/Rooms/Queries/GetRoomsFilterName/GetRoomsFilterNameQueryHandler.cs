using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRoomsFilterName;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRoomsFilterName;

public class GetRoomsFilterNameQueryHandler
    : IRequestHandler<GetRoomsFilterNameQuery, GetRoomsFilterNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRoomsFilterNameQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetRoomsFilterNameResponse> Handle(
        GetRoomsFilterNameQuery query,
        CancellationToken cancellationToken)
    {
        GetRoomsFilterNameRequest request = query.GetRoomsFilterNameRequest;
        var response = new GetRoomsFilterNameResponse(request.CorrelationId);

        var specification = new RoomNamesSpecification(request.NameFilterValue);

        var roomNames = await _unitOfWork
            .ReadRepository<Room>()
            .ListAsync(specification, cancellationToken);

        if (roomNames is null)
            return response;

        response.RoomNames = roomNames;
        return response;
    }
}