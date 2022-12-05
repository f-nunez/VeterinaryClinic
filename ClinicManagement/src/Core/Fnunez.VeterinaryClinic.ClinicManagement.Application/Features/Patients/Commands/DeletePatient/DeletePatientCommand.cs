using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.DeletePatient;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.DeletePatient;

public record DeletePatientCommand(DeletePatientRequest DeletePatientRequest) : IRequest<DeletePatientResponse>;