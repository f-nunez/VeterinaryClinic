using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.UpdateDoctor;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.UpdateDoctor;

public record UpdateDoctorCommand(UpdateDoctorRequest UpdateDoctorRequest) : IRequest<UpdateDoctorResponse>;