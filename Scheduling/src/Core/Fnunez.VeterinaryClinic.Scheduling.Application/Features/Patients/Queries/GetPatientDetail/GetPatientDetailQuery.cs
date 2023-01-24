using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatientDetail;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.Queries.GetPatientDetail;

public record GetPatientDetailQuery(GetPatientDetailRequest GetPatientDetailRequest)
    : IRequest<GetPatientDetailResponse>;