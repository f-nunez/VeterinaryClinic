using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomById;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Queries.GetRoomById;

public record GetRoomByIdQuery(GetRoomByIdRequest GetByIdRoomRequest) : IRequest<GetRoomByIdResponse>;