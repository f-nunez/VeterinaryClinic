using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Schedules.Commands.CreateSchedule;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Schedules.Commands.DeleteSchedule;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Schedules.Commands.UpdateSchedule;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Schedules.Queries.GetScheduleById;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Schedules.Queries.GetSchedules;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.CreateSchedule;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.DeleteSchedule;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.GetScheduleById;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.GetSchedules;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.UpdateSchedule;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.Scheduling.Api.Controllers;

public class ScheduleController : BaseApiController
{
    [HttpPost("Create")]
    public async Task<ActionResult> Create(
        CreateScheduleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateScheduleCommand(request);

        CreateScheduleResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("Delete/{Id}")]
    public async Task<ActionResult> Delete(
        [FromRoute] DeleteScheduleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeleteScheduleCommand(request);

        DeleteScheduleResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpGet("GetById")]
    public async Task<ActionResult> GetById(
        [FromQuery] GetScheduleByIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetScheduleByIdQuery(request);

        GetScheduleByIdResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("List")]
    public async Task<ActionResult> List(
        [FromQuery] GetSchedulesRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetSchedulesQuery(request);

        GetSchedulesResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<ActionResult> Update(
        UpdateScheduleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateScheduleCommand(request);

        UpdateScheduleResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }
}