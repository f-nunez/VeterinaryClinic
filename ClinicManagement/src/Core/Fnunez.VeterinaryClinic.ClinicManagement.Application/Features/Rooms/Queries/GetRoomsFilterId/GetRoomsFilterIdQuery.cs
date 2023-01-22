using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomsFilterId;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Queries.GetRoomsFilterId;

public record GetRoomsFilterIdQuery(GetRoomsFilterIdRequest GetRoomsFilterIdRequest)
    : IRequest<GetRoomsFilterIdResponse>;