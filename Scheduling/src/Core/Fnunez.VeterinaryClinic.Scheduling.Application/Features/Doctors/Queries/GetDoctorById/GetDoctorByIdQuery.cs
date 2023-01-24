using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctorById;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctorById;

public record GetDoctorByIdQuery(GetDoctorByIdRequest GetDoctorByIdRequest)
    : IRequest<GetDoctorByIdResponse>;