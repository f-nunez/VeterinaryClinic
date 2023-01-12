using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRoomsFilterName;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRoomsFilterName;

public record GetRoomsFilterNameQuery(GetRoomsFilterNameRequest GetRoomsFilterNameRequest)
    : IRequest<GetRoomsFilterNameResponse>;