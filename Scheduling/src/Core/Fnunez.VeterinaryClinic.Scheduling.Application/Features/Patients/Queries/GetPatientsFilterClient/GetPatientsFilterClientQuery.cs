using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatientsFilterClient;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.Queries.GetPatientsFilterClient;

public record GetPatientsFilterClientQuery(GetPatientsFilterClientRequest GetPatientsFilterClientRequest)
    : IRequest<GetPatientsFilterClientResponse>;