using Contracts;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.SendIntegrationEvents.PatientCreated;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Interfaces.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Interfaces.Settings;
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
    private readonly IClientStorageSetting _clientStorageSetting;
    private readonly IFileSystemWriterService _fileSystemWriterService;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePatientCommandHandler(
        IClientStorageSetting clientStorageSetting,
        IFileSystemWriterService fileSystemWriterService,
        IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _clientStorageSetting = clientStorageSetting;
        _fileSystemWriterService = fileSystemWriterService;
        _mediator = mediator;
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

        string savedPhotoName = await SavePhotoAsync(request);

        var newPatient = new Patient(
            request.ClientId,
            request.Name,
            (AnimalSex)Enum.ToObject(typeof(AnimalSex), request.Sex),
            new AnimalType(request.Breed, request.Species),
            new Photo(request.PhotoName, savedPhotoName),
            preferredDoctorId: request.PreferredDoctorId
        );

        client.AddPatient(newPatient);

        await _unitOfWork.Repository<Client>()
            .UpdateAsync(client, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        await SendIntegrationEventAsync(
            newPatient,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }

    private async Task<string> SavePhotoAsync(CreatePatientRequest request)
    {
        string photoExtension = Path.GetExtension(request.PhotoName).ToLower();

        string photoNameToSave = $"{Guid.NewGuid().ToString()}{photoExtension}";

        string relativePhotoPath = Path.Combine(
            request.ClientId.ToString(), photoNameToSave);

        string photoPath = Path.Combine(
            _clientStorageSetting.BasePath, relativePhotoPath);

        await _fileSystemWriterService.WriteAsync(request.PhotoData, photoPath);

        return photoNameToSave;
    }

    private async Task SendIntegrationEventAsync(
        Patient patient,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        var message = new PatientCreatedIntegrationEventContract
        {
            CausationId = correlationId,
            CorrelationId = correlationId,
            Id = Guid.NewGuid(),
            OccurredOn = DateTimeOffset.UtcNow,
            PatientBreed = patient.AnimalType.Breed,
            PatientClientId = patient.ClientId,
            PatientId = patient.Id,
            PatientName = patient.Name,
            PatientPhotoName = patient.Photo.Name,
            PatientPhotoStoredName = patient.Photo.StoredName,
            PatientPreferredDoctorId = patient.PreferredDoctorId,
            PatientSex = (int)patient.AnimalSex,
            PatientSpecies = patient.AnimalType.Species
        };

        await _mediator.Publish(
            new PatientCreatedSendIntegrationEvent(message),
            cancellationToken
        );
    }
}