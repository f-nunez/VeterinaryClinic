using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomById;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Queries.GetRoomById;

public class GetRoomByIdQueryHandler
    : IRequestHandler<GetRoomByIdQuery, GetRoomByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetRoomByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetRoomByIdResponse> Handle(
        GetRoomByIdQuery query,
        CancellationToken cancellationToken)
    {
        GetRoomByIdRequest request = query.GetByIdRoomRequest;
        var response = new GetRoomByIdResponse(request.CorrelationId);

        var room = await _unitOfWork.ReadRepository<Room>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (room is null)
            throw new NotFoundException(nameof(room), request.Id);

        response.Room = _mapper.Map<RoomDto>(room);

        return response;
    }
}