using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.UpdatePatient;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.UpdatePatient;

public record UpdatePatientCommand(UpdatePatientRequest UpdatePatientRequest) : IRequest<UpdatePatientResponse>;