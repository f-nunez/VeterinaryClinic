using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorById;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctorById;

public record GetDoctorByIdQuery(GetDoctorByIdRequest GetDoctorByIdRequest) : IRequest<GetDoctorByIdResponse>;