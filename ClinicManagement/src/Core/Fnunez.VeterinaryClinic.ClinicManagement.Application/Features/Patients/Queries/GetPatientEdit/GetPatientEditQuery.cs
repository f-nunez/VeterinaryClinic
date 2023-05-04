using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientEdit;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientEdit;

public record GetPatientEditQuery(GetPatientEditRequest GetPatientEditRequest)
    : IRequest<GetPatientEditResponse>;