using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRoomById;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRooms;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRoomsFilterId;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRoomsFilterName;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRoomById;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRooms;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRoomsFilterId;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRoomsFilterName;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.Scheduling.Api.Controllers;

public class RoomController : BaseApiController
{
    [HttpPost("DataGrid")]
    public async Task<ActionResult> DataGrid(
        GetRoomsRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetRoomsQuery(request);

        GetRoomsResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterId")]
    public async Task<ActionResult> DataGridFilterId(
        GetRoomsFilterIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetRoomsFilterIdQuery(request);

        GetRoomsFilterIdResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterName")]
    public async Task<ActionResult> DataGridFilterName(
        GetRoomsFilterNameRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetRoomsFilterNameQuery(request);

        GetRoomsFilterNameResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("GetById/{Id}")]
    public async Task<ActionResult> GetById(
        [FromRoute] GetRoomByIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetRoomByIdQuery(request);

        GetRoomByIdResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }
}