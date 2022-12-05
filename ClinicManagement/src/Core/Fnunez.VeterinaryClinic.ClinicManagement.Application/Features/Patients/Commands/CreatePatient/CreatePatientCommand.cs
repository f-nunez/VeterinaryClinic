using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.CreatePatient;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.CreatePatient;

public record CreatePatientCommand(CreatePatientRequest CreatePatientRequest) : IRequest<CreatePatientResponse>;