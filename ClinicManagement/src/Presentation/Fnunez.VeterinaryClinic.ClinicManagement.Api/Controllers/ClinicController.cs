using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Commands.CreateClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Commands.DeleteClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Commands.UpdateClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinics;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterAddress;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterEmailAddress;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.CreateClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.DeleteClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinics;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterAddress;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterEmailAddress;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.UpdateClinic;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Api.Controllers;

public class ClinicController : BaseApiController
{
    [HttpPost("Create")]
    public async Task<ActionResult> Create(
        CreateClinicRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateClinicCommand(request);

        CreateClinicResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

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

    [HttpDelete("Delete/{Id}")]
    public async Task<ActionResult> Delete(
        [FromRoute] DeleteClinicRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeleteClinicCommand(request);

        DeleteClinicResponse response = await Mediator
            .Send(command, cancellationToken);

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

    [HttpPut("Update")]
    public async Task<ActionResult> Update(
        UpdateClinicRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateClinicCommand(request);

        UpdateClinicResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }
}