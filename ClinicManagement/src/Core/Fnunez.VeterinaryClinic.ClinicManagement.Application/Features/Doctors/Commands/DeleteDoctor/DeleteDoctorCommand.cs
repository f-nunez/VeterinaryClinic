using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.DeleteDoctor;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.DeleteDoctor;

public record DeleteDoctorCommand(DeleteDoctorRequest DeleteDoctorRequest) : IRequest<DeleteDoctorResponse>;