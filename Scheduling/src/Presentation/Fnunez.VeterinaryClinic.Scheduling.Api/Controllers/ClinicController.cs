using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicById;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinics;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicsFilterAddress;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicsFilterEmailAddress;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicsFilterId;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicsFilterName;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicById;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinics;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicsFilterAddress;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicsFilterEmailAddress;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicsFilterId;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicsFilterName;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.Scheduling.Api.Controllers;

public class ClinicController : BaseApiController
{
    [HttpPost("DataGrid")]
    public async Task<ActionResult> DataGrid(
        GetClinicsRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClinicsQuery(request);

        GetClinicsResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterAddress")]
    public async Task<ActionResult> DataGridFilterAddress(
        GetClinicsFilterAddressRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClinicsFilterAddressQuery(request);

        GetClinicsFilterAddressResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterEmailAddress")]
    public async Task<ActionResult> DataGridFilterEmailAddress(
        GetClinicsFilterEmailAddressRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClinicsFilterEmailAddressQuery(request);

        GetClinicsFilterEmailAddressResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterId")]
    public async Task<ActionResult> DataGridFilterId(
        GetClinicsFilterIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClinicsFilterIdQuery(request);

        GetClinicsFilterIdResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterName")]
    public async Task<ActionResult> DataGridFilterName(
        GetClinicsFilterNameRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClinicsFilterNameQuery(request);

        GetClinicsFilterNameResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("GetById/{Id}")]
    public async Task<ActionResult> GetById(
        [FromRoute] GetClinicByIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetClinicByIdQuery(request);

        GetClinicByIdResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }
}