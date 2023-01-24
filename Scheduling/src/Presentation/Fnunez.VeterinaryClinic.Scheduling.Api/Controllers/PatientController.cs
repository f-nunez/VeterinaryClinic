using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.Queries.GetPatientDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.Queries.GetPatients;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.Queries.GetPatientsFilterClient;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatientDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatients;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatientsFilterClient;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.Scheduling.Api.Controllers;

public class PatientController : BaseApiController
{
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
}