using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.CreateRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler
    : IRequestHandler<CreateRoomCommand, CreateRoomResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoomCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateRoomResponse> Handle(
        CreateRoomCommand command,
        CancellationToken cancellationToken)
    {
        CreateRoomRequest request = command.CreateRoomRequest;
        var response = new CreateRoomResponse(request.CorrelationId);
        var newRoom = _mapper.Map<Room>(request);

        newRoom = await _unitOfWork.Repository<Room>()
            .AddAsync(newRoom, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        var roomDto = _mapper.Map<RoomDto>(newRoom);
        response.Room = roomDto;

        return response;
    }
}