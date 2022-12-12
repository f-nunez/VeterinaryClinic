using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.DeleteAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.UpdateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentById;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointments;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.DeleteAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentById;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointments;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.UpdateAppointment;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.Scheduling.Api.Controllers;

public class AppointmentController : BaseApiController
{
    [HttpPost("Create")]
    public async Task<ActionResult> Create(
        CreateAppointmentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateAppointmentCommand(request);

        CreateAppointmentResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("Delete/{AppointmentId}/Schedule/{ScheduleId}")]
    public async Task<ActionResult> Delete(
        [FromRoute] DeleteAppointmentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeleteAppointmentCommand(request);

        DeleteAppointmentResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpGet("GetById")]
    public async Task<ActionResult> GetById(
        [FromQuery] GetAppointmentByIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentByIdQuery(request);

        GetAppointmentByIdResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("List")]
    public async Task<ActionResult> List(
        [FromQuery] GetAppointmentsRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentsQuery(request);

        GetAppointmentsResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<ActionResult> Update(
        UpdateAppointmentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateAppointmentCommand(request);

        UpdateAppointmentResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }
}