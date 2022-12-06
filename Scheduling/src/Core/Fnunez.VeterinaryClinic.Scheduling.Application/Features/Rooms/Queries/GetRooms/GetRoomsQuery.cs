using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRooms;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRooms;

public record GetRoomsQuery(GetRoomsRequest GetRoomsRequest) : IRequest<GetRoomsResponse>;