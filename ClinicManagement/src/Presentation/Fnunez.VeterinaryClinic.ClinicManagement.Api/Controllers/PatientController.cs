using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.CreatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.DeletePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.UpdatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.CreatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.DeletePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.UpdatePatient;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Api.Controllers;

public class PatientController : BaseApiController
{
    [HttpPost("Create")]
    public async Task<ActionResult> Create(
        CreatePatientRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreatePatientCommand(request);

        CreatePatientResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> Delete(
        DeletePatientRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeletePatientCommand(request);

        DeletePatientResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpGet("GetById")]
    public async Task<ActionResult> GetById(
        [FromQuery] GetPatientByIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetPatientByIdQuery(request);

        GetPatientByIdResponse response = await Mediator
            .Send(query, cancellationToken);

        if (response.Patient is null)
            return NotFound();

        return Ok(response);
    }

    [HttpGet("List")]
    public async Task<ActionResult> List(
        [FromQuery] GetPatientsRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetPatientsQuery(request);

        GetPatientsResponse response = await Mediator
            .Send(query, cancellationToken);

        if (response.Patients is null)
            return NotFound();

        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<ActionResult> Update(
        UpdatePatientRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdatePatientCommand(request);

        UpdatePatientResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }
}