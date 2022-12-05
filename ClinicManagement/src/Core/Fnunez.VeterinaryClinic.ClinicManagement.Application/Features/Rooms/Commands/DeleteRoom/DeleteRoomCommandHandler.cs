using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.DeleteRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Commands.DeleteRoom;

public class DeleteRoomCommandHandler
    : IRequestHandler<DeleteRoomCommand, DeleteRoomResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoomCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteRoomResponse> Handle(
        DeleteRoomCommand command,
        CancellationToken cancellationToken)
    {
        DeleteRoomRequest request = command.DeleteRoomRequest;
        var response = new DeleteRoomResponse(request.CorrelationId);
        var roomToDelete = _mapper.Map<Room>(request);

        await _unitOfWork.Repository<Room>()
            .DeleteAsync(roomToDelete, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return response;
    }
}