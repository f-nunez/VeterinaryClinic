using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientById;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientById;

public record GetPatientByIdQuery(GetPatientByIdRequest GetPatientByIdRequest) : IRequest<GetPatientByIdResponse>;