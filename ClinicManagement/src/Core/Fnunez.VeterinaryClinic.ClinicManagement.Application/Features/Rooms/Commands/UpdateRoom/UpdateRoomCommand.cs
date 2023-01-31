using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.UpdateRoom;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Commands.UpdateRoom;

public record UpdateRoomCommand(UpdateRoomRequest UpdateRoomRequest)
    : IRequest<UpdateRoomResponse>;