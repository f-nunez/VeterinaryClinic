using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRooms;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRooms;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.Scheduling.Api.Controllers;

public class RoomController : BaseApiController
{
    [HttpGet("List")]
    public async Task<ActionResult> List(
        [FromQuery] GetRoomsRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetRoomsQuery(request);

        GetRoomsResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }
}