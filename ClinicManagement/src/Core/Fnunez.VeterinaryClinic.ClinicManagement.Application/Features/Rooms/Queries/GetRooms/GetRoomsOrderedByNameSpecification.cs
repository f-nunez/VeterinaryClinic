using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Queries.GetRooms;

public class GetRoomsOrderedByNameSpecification : BaseSpecification<Room>
{
    public GetRoomsOrderedByNameSpecification()
    {
        Query.OrderBy(room => room.Name);
    }
}