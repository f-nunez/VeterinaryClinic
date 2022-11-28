using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatients;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatients;

public record GetPatientsQuery(GetPatientsRequest GetPatientsRequest) : IRequest<GetPatientsResponse>;