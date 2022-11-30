using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.CreateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.DeleteDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.UpdateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctorById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctors;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.CreateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.DeleteDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctors;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.UpdateDoctor;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Api.Controllers;

public class DoctorController : BaseApiController
{
    [HttpPost("Create")]
    public async Task<ActionResult> Create(
        CreateDoctorRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateDoctorCommand(request);

        CreateDoctorResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> Delete(
        [FromRoute] DeleteDoctorRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeleteDoctorCommand(request);

        DeleteDoctorResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpGet("GetById")]
    public async Task<ActionResult> GetById(
        [FromQuery] GetDoctorByIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetDoctorByIdQuery(request);

        GetDoctorByIdResponse response = await Mediator
            .Send(query, cancellationToken);

        if (response.Doctor is null)
            return NotFound();

        return Ok(response);
    }

    [HttpGet("List")]
    public async Task<ActionResult> List(
        [FromQuery] GetDoctorsRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetDoctorsQuery(request);

        GetDoctorsResponse response = await Mediator
            .Send(query, cancellationToken);

        if (response.Doctors is null)
            return NotFound();

        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<ActionResult> Update(
        UpdateDoctorRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateDoctorCommand(request);

        UpdateDoctorResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }
}