using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.CreateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.DeleteAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.UpdateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypeById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypes;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterCode;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterDuration;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.CreateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.DeleteAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypeById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypes;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterCode;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterDuration;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterName;
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

    [HttpPost("DataGrid")]
    public async Task<ActionResult> DataGrid(
        GetAppointmentTypesRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentTypesQuery(request);

        GetAppointmentTypesResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterCode")]
    public async Task<ActionResult> DataGridFilterCode(
        GetAppointmentTypesFilterCodeRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentTypesFilterCodeQuery(request);

        GetAppointmentTypesFilterCodeResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterDuration")]
    public async Task<ActionResult> DataGridFilterDuration(
        GetAppointmentTypesFilterDurationRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentTypesFilterDurationQuery(request);

        GetAppointmentTypesFilterDurationResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterId")]
    public async Task<ActionResult> DataGridFilterId(
        GetAppointmentTypesFilterIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentTypesFilterIdQuery(request);

        GetAppointmentTypesFilterIdResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterName")]
    public async Task<ActionResult> DataGridFilterName(
        GetAppointmentTypesFilterNameRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentTypesFilterNameQuery(request);

        GetAppointmentTypesFilterNameResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("Delete/{Id}")]
    public async Task<ActionResult> Delete(
        [FromRoute] DeleteAppointmentTypeRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeleteAppointmentTypeCommand(request);

        DeleteAppointmentTypeResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpGet("GetById/{Id}")]
    public async Task<ActionResult> GetById(
        [FromRoute] GetAppointmentTypeByIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppointmentTypeByIdQuery(request);

        GetAppointmentTypeByIdResponse response = await Mediator
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