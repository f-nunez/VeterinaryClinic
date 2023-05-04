using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRooms;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRooms;

public class GetRoomsQueryHandler
    : IRequestHandler<GetRoomsQuery, GetRoomsResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetRoomsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetRoomsResponse> Handle(
        GetRoomsQuery query,
        CancellationToken cancellationToken)
    {
        GetRoomsRequest request = query.GetRoomsRequest;
        var response = new GetRoomsResponse(request.CorrelationId);

        var specification = new RoomsSpecification(
            request);

        var rooms = await _unitOfWork
            .ReadRepository<Room>()
            .ListAsync(specification, cancellationToken);

        int count = await _unitOfWork
            .ReadRepository<Room>()
            .CountAsync(specification, cancellationToken);

        if (rooms is null)
            return response;

        var roomDtos = _mapper
            .Map<List<RoomDto>>(rooms);

        response.DataGridResponse = new DataGridResponse<RoomDto>(
            roomDtos,
            count
        );

        return response;
    }
}