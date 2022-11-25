using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.UpdateRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Commands.UpdateRoom;

public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, UpdateRoomResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoomCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateRoomResponse> Handle(
        UpdateRoomCommand command,
        CancellationToken cancellationToken)
    {
        UpdateRoomRequest request = command.UpdateRoomRequest;
        var response = new UpdateRoomResponse(request.CorrelationId);
        var roomToUpdate = _mapper.Map<Room>(request);

        await _unitOfWork.Repository<Room>()
            .UpdateAsync(roomToUpdate, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        var roomDto = _mapper.Map<RoomDto>(roomToUpdate);
        response.Room = roomDto;

        return response;
    }
}
