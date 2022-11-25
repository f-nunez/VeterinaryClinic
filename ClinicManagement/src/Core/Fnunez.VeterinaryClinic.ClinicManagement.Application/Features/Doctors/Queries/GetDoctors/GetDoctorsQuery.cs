using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctors;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctors;

public record GetDoctorsQuery(GetDoctorsRequest GetDoctorsRequest) : IRequest<GetDoctorsResponse>;