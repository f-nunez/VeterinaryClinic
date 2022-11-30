using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Commands.CreateRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Commands.DeleteRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Commands.UpdateRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Queries.GetRoomById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Queries.GetRooms;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.CreateRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.DeleteRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRooms;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.UpdateRoom;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Api.Controllers;

public class RoomController : BaseApiController
{
    [HttpPost("Create")]
    public async Task<ActionResult> Create(
        CreateRoomRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateRoomCommand(request);

        CreateRoomResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> Delete(
        [FromRoute] DeleteRoomRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeleteRoomCommand(request);

        DeleteRoomResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpGet("GetById")]
    public async Task<ActionResult> GetById(
        [FromQuery] GetRoomByIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetRoomByIdQuery(request);

        GetRoomByIdResponse response = await Mediator
            .Send(query, cancellationToken);

        if (response.Room is null)
            return NotFound();

        return Ok(response);
    }

    [HttpGet("List")]
    public async Task<ActionResult> List(
        [FromQuery] GetRoomsRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetRoomsQuery(request);

        GetRoomsResponse response = await Mediator
            .Send(query, cancellationToken);

        if (response.Rooms is null)
            return NotFound();

        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<ActionResult> Update(
        UpdateRoomRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateRoomCommand(request);

        UpdateRoomResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }
}