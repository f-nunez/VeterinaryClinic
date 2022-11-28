using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.CreatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.DeletePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.DeletePatient;

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, DeletePatientResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePatientCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DeletePatientResponse> Handle(
        DeletePatientCommand command,
        CancellationToken cancellationToken)
    {
        DeletePatientRequest request = command.DeletePatientRequest;
        var response = new DeletePatientResponse(request.CorrelationId);
        var specification = new ClientByIdIncludePatientsSpecification(request.ClientId);

        var client = await _unitOfWork.Repository<Client>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (client is null)
            return response;

        var patientToDelete = client.Patients
            .FirstOrDefault(p => p.Id == request.PatientId);

        if (patientToDelete is null)
            return response;

        client.RemovePatient(patientToDelete);

        await _unitOfWork.Repository<Client>()
            .UpdateAsync(client);
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return response;
    }
}
