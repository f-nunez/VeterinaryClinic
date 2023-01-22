using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomsFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Queries.GetRoomsFilterId;

public class GetRoomsFilterIdQueryHandler
    : IRequestHandler<GetRoomsFilterIdQuery, GetRoomsFilterIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRoomsFilterIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetRoomsFilterIdResponse> Handle(
        GetRoomsFilterIdQuery query,
        CancellationToken cancellationToken)
    {
        GetRoomsFilterIdRequest request = query.GetRoomsFilterIdRequest;
        var response = new GetRoomsFilterIdResponse(request.CorrelationId);

        var specification = new RoomIdsSpecification(request.IdFilterValue);

        var roomIds = await _unitOfWork
            .ReadRepository<Room>()
            .ListAsync(specification, cancellationToken);

        if (roomIds is null)
            return response;

        response.RoomIds = roomIds;

        return response;
    }
}