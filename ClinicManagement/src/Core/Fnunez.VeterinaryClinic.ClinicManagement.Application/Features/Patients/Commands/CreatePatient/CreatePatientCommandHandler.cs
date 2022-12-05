using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.CreatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.ValueObjects;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.CreatePatient;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, CreatePatientResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePatientCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreatePatientResponse> Handle(
        CreatePatientCommand command,
        CancellationToken cancellationToken)
    {
        CreatePatientRequest request = command.CreatePatientRequest;
        var response = new CreatePatientResponse(request.CorrelationId);
        var specification = new ClientByIdIncludePatientsSpecification(request.ClientId);

        var client = await _unitOfWork.Repository<Client>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (client is null)
            return response;

        var newPatient = new Patient(
            clientId: request.ClientId,
            name: request.PatientName,
            animalSex: AnimalSex.Female,
            animalType: new AnimalType("Dog", "Chihuahua"),
            preferredDoctorId: request.PreferredDoctorId
        );

        client.AddPatient(newPatient);

        await _unitOfWork.Repository<Client>()
            .UpdateAsync(client, cancellationToken);
        
        await _unitOfWork.CommitAsync(cancellationToken);

        response.Patient = _mapper.Map<PatientDto>(newPatient);

        return response;
    }
}
