using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRoomById;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRoomById;

public record GetRoomByIdQuery(GetRoomByIdRequest GetByIdRoomRequest)
    : IRequest<GetRoomByIdResponse>;