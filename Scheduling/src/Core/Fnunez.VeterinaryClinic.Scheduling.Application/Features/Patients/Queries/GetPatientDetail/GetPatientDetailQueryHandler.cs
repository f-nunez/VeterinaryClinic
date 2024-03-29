using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services;
using Fnunez.VeterinaryClinic.Scheduling.Application.Settings;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatientDetail;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.Queries.GetPatientDetail;

public class GetPatientDetailQueryHandler
    : IRequestHandler<GetPatientDetailQuery, GetPatientDetailResponse>
{
    private readonly IClientStorageSetting _clientStorageSetting;
    private readonly IFileSystemReaderService _fileSystemReaderService;
    private readonly ILogger<GetPatientDetailQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetPatientDetailQueryHandler(
        IClientStorageSetting clientStorageSetting,
        IFileSystemReaderService fileSystemReaderService,
        ILogger<GetPatientDetailQueryHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _clientStorageSetting = clientStorageSetting;
        _fileSystemReaderService = fileSystemReaderService;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetPatientDetailResponse> Handle(
        GetPatientDetailQuery query,
        CancellationToken cancellationToken)
    {
        GetPatientDetailRequest request = query.GetPatientDetailRequest;
        var response = new GetPatientDetailResponse(request.CorrelationId);
        var specification = new ClientByIdSpecification(request.ClientId);

        var client = await _unitOfWork
            .ReadRepository<Client>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (client is null)
            throw new NotFoundException(nameof(client), request.ClientId);

        var patient = client.Patients
            .FirstOrDefault(p => p.Id == request.PatientId);

        if (patient is null)
            throw new NotFoundException(nameof(patient), request.PatientId);

        string doctorFullName = await GetPreferredDoctorFullNameAsync(
            patient, cancellationToken);

        response.PatientDetail = await MapPatientDetailDtoAsync(
            patient, doctorFullName);

        return response;
    }

    private async Task<string> GetPreferredDoctorFullNameAsync(
        Patient patient,
        CancellationToken cancellationToken)
    {
        if (!patient.PreferredDoctorId.HasValue)
            return string.Empty;

        var preferredDoctor = await _unitOfWork
            .ReadRepository<Doctor>()
            .GetByIdAsync(patient.PreferredDoctorId.Value, cancellationToken);

        if (preferredDoctor is null)
            throw new NotFoundException(
                nameof(preferredDoctor), patient.PreferredDoctorId.Value);

        return preferredDoctor.FullName;
    }

    private async Task<PatientDetailDto> MapPatientDetailDtoAsync(
        Patient patient,
        string doctorFullName)
    {
        byte[]? photoData = patient.IsActive
            ? await GetPhotoDataAsync(patient)
            : null;

        return new PatientDetailDto
        {
            Breed = patient.AnimalType.Breed,
            ClientId = patient.ClientId,
            Name = patient.Name,
            PatientId = patient.Id,
            PhotoData = photoData,
            PhotoName = patient.Photo.Name,
            PreferredDoctorFullName = doctorFullName,
            Sex = (int)patient.AnimalSex,
            Species = patient.AnimalType.Species
        };
    }

    private async Task<byte[]?> GetPhotoDataAsync(Patient patient)
    {
        string relativePhotoPath = Path.Combine(
            patient.ClientId.ToString(), patient.Photo.StoredName);

        string photoPath = Path.Combine(
            _clientStorageSetting.BasePath, relativePhotoPath);

        byte[]? photoData = null;

        try
        {
            photoData = await _fileSystemReaderService.ReadAsync(photoPath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }

        return photoData;
    }
}