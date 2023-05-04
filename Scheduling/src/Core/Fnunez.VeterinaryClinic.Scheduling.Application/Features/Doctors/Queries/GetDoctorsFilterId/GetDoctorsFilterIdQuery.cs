using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctorsFilterId;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctorsFilterId;

public record GetDoctorsFilterIdQuery(GetDoctorsFilterIdRequest GetDoctorsFilterIdRequest)
    : IRequest<GetDoctorsFilterIdResponse>;