using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctors;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctors;

public record GetDoctorsQuery(GetDoctorsRequest GetDoctorsRequest)
    : IRequest<GetDoctorsResponse>;