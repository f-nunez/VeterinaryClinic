using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.CreatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.UpdatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.UpdatePatient;

public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, UpdatePatientResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePatientCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdatePatientResponse> Handle(
        UpdatePatientCommand command,
        CancellationToken cancellationToken)
    {
        UpdatePatientRequest request = command.UpdatePatientRequest;
        var response = new UpdatePatientResponse(request.CorrelationId);
        var specification = new ClientByIdIncludePatientsSpecification(request.ClientId);

        var client = await _unitOfWork.Repository<Client>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (client is null)
            return response;

        var patientToUpdate = client.Patients
            .FirstOrDefault(p => p.Id == request.PatientId);

        if (patientToUpdate is null)
            return response;

        patientToUpdate.UpdateName(request.Name);

        await _unitOfWork.Repository<Client>()
            .UpdateAsync(client, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return response;
    }
}
