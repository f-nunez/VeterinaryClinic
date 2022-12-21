using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatients;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.Queries.GetPatients;

public record GetPatientsQuery(GetPatientsRequest GetPatientsRequest)
    : IRequest<GetPatientsResponse>;