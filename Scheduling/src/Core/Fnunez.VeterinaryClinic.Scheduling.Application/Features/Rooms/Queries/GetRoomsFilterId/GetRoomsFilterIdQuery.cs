using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRoomsFilterId;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRoomsFilterId;

public record GetRoomsFilterIdQuery(GetRoomsFilterIdRequest GetRoomsFilterIdRequest)
    : IRequest<GetRoomsFilterIdResponse>;