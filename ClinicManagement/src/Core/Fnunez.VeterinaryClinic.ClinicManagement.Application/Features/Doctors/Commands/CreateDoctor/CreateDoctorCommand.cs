using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.CreateDoctor;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.CreateDoctor;

public record CreateDoctorCommand(CreateDoctorRequest CreateDoctorRequest)
    : IRequest<CreateDoctorResponse>;