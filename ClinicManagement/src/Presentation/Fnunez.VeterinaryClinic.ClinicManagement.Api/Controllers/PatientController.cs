using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.CreatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.DeletePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.UpdatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientDetail;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientEdit;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientsFilterClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientsFilterPreferredDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.CreatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.DeletePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientDetail;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientEdit;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientsFilterClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientsFilterPreferredDoctor;
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

    [HttpPost("DataGridFilterClient")]
    public async Task<ActionResult> DataGridFilterClient(
        GetPatientsFilterClientRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetPatientsFilterClientQuery(request);

        GetPatientsFilterClientResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterPreferredDoctor")]
    public async Task<ActionResult> DataGridFilterPreferredDoctor(
        GetPatientsFilterPreferredDoctorRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetPatientsFilterPreferredDoctorQuery(request);

        GetPatientsFilterPreferredDoctorResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("Delete/{PatientId}/Client/{ClientId}")]
    public async Task<ActionResult> Delete(
        [FromRoute] DeletePatientRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeletePatientCommand(request);

        DeletePatientResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpGet("GetById/{Id}")]
    public async Task<ActionResult> GetById(
        [FromRoute] GetPatientByIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetPatientByIdQuery(request);

        GetPatientByIdResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("GetPatientDetail/{PatientId}/Client/{ClientId}")]
    public async Task<ActionResult> GetPatientDetail(
        [FromRoute] GetPatientDetailRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetPatientDetailQuery(request);

        GetPatientDetailResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("GetPatientEdit/{PatientId}/Client/{ClientId}")]
    public async Task<ActionResult> GetPatientEdit(
        [FromRoute] GetPatientEditRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetPatientEditQuery(request);

        GetPatientEditResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("GetPatients/{ClientId}")]
    public async Task<ActionResult> GetPatients(
        [FromRoute] GetPatientsRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetPatientsQuery(request);

        GetPatientsResponse response = await Mediator
            .Send(query, cancellationToken);

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