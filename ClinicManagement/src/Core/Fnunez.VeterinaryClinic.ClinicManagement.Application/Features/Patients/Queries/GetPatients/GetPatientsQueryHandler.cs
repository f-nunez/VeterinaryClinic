using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Settings;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatients;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatients;

public class GetPatientsQueryHandler
    : IRequestHandler<GetPatientsQuery, GetPatientsResponse>
{
    private readonly IClientStorageSetting _clientStorageSetting;
    private readonly IFileSystemReaderService _fileSystemReaderService;
    private readonly ILogger<GetPatientsQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetPatientsQueryHandler(
        IClientStorageSetting clientStorageSetting,
        IFileSystemReaderService fileSystemReaderService,
        ILogger<GetPatientsQueryHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _clientStorageSetting = clientStorageSetting;
        _fileSystemReaderService = fileSystemReaderService;
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetPatientsResponse> Handle(
        GetPatientsQuery query,
        CancellationToken cancellationToken)
    {
        GetPatientsRequest request = query.GetPatientsRequest;
        var response = new GetPatientsResponse(request.CorrelationId);

        var specification = new ClientByIdSpecification(request.ClientId);

        var client = await _unitOfWork
            .ReadRepository<Client>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (client is null)
            throw new NotFoundException(nameof(client), request.ClientId);

        if (client.Patients is null)
            return response;

        response.Count = response.Patients.Count;

        response.Patients = await MapPatientsDtos(client.Patients);

        return response;
    }

    private async Task<List<PatientsDto>> MapPatientsDtos(
        IReadOnlyList<Patient> patients)
    {
        var patientsDtos = new List<PatientsDto>();

        foreach (Patient patient in patients.Where(p => p.IsActive))
        {
            byte[]? photoData = patient.IsActive
                ? await GetPhotoDataAsync(patient)
                : null;

            var patientsDto = new PatientsDto
            {
                ClientId = patient.ClientId,
                Name = patient.Name,
                PatientId = patient.Id,
                PhotoData = photoData,
                PhotoName = patient.Photo.Name,
                Sex = (int)patient.AnimalSex,
                Species = patient.AnimalType.Species
            };

            patientsDtos.Add(patientsDto);
        }

        return patientsDtos;
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
