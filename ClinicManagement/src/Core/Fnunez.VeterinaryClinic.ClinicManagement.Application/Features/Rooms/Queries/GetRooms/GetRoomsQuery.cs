using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRooms;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Queries.GetRooms;

public record GetRoomsQuery(GetRoomsRequest GetRoomsRequest) : IRequest<GetRoomsResponse>;