using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.CreateRoom;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Commands.CreateRoom;

public record CreateRoomCommand(CreateRoomRequest CreateRoomRequest) : IRequest<CreateRoomResponse>;