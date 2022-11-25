using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.DeleteRoom;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Commands.DeleteRoom;

public record DeleteRoomCommand(DeleteRoomRequest DeleteRoomRequest) : IRequest<DeleteRoomResponse>;