using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.CreateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.DeleteDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.UpdateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctorById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctors;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctorsFilterFullName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctorsFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.CreateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.DeleteDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctors;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorsFilterFullName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorsFilterId;
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

    [HttpPost("DataGrid")]
    public async Task<ActionResult> DataGrid(
        GetDoctorsRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetDoctorsQuery(request);

        GetDoctorsResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterFullName")]
    public async Task<ActionResult> DataGridFilterFullName(
        GetDoctorsFilterFullNameRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetDoctorsFilterFullNameQuery(request);

        GetDoctorsFilterFullNameResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("DataGridFilterId")]
    public async Task<ActionResult> DataGridFilterId(
        GetDoctorsFilterIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetDoctorsFilterIdQuery(request);

        GetDoctorsFilterIdResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("Delete/{Id}")]
    public async Task<ActionResult> Delete(
        [FromRoute] DeleteDoctorRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeleteDoctorCommand(request);

        DeleteDoctorResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpGet("GetById/{Id}")]
    public async Task<ActionResult> GetById(
        [FromRoute] GetDoctorByIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetDoctorByIdQuery(request);

        GetDoctorByIdResponse response = await Mediator
            .Send(query, cancellationToken);

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