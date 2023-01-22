using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomsFilterName;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Queries.GetRoomsFilterName;

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