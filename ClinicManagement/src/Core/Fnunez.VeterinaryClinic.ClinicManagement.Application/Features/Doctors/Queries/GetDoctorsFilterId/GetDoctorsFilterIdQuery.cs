using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorsFilterId;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctorsFilterId;

public record GetDoctorsFilterIdQuery(GetDoctorsFilterIdRequest GetDoctorsFilterIdRequest)
    : IRequest<GetDoctorsFilterIdResponse>;