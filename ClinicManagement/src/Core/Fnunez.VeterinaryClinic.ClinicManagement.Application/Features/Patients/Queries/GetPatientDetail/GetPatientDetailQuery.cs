using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientDetail;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientDetail;

public record GetPatientDetailQuery(GetPatientDetailRequest GetPatientDetailRequest)
    : IRequest<GetPatientDetailResponse>;