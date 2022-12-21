using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctorsFilterFullName;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctorsFilterFullName;

public record GetDoctorsFilterFullNameQuery(GetDoctorsFilterFullNameRequest GetDoctorsFilterFullNameRequest)
    : IRequest<GetDoctorsFilterFullNameResponse>;