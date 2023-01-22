using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomsFilterName;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Queries.GetRoomsFilterName;

public record GetRoomsFilterNameQuery(GetRoomsFilterNameRequest GetRoomsFilterNameRequest)
    : IRequest<GetRoomsFilterNameResponse>;