using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.CreateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.DeleteAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.UpdateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypes;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.CreateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.DeleteAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypeById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypes;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.UpdateAppointmentType;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Api.Controllers;

public class AppointmentTypeController : BaseApiController
{
    [HttpPost("Create")]
    public async Task<ActionResult> Create(
        CreateAppointmentTypeRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateAppointmentTypeCommand(request);

        CreateAppointmentTypeResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> Delete(
        [FromRoute] DeleteAppointmentTypeRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeleteAppointmentTypeCommand(request);

        DeleteAppointmentTypeResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpGet("GetById")]
    public async Task<ActionResult> GetById(
        [FromQuery] GetAppointmentTypeByIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentTypeByIdQuery(request);

        GetAppointmentTypeByIdResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("List")]
    public async Task<ActionResult> List(
        [FromQuery] GetAppointmentTypesRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentTypesQuery(request);

        GetAppointmentTypesResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<ActionResult> Update(
        UpdateAppointmentTypeRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateAppointmentTypeCommand(request);

        UpdateAppointmentTypeResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }
}