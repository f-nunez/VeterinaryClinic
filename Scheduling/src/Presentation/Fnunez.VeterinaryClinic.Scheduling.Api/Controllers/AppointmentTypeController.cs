using Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypes;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterCode;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterDuration;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterId;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterName;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypes;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterCode;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterDuration;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterId;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterName;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.Scheduling.Api.Controllers;

public class AppointmentTypeController : BaseApiController
{
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
}