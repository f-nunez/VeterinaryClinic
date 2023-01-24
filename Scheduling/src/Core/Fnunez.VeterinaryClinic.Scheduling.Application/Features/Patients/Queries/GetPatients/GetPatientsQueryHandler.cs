using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Interfaces.Services;
using Fnunez.VeterinaryClinic.Scheduling.Application.Interfaces.Settings;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatients;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.Queries.GetPatients;

public class GetPatientsQueryHandler
    : IRequestHandler<GetPatientsQuery, GetPatientsResponse>
{
    private readonly IClientStorageSetting _clientStorageSetting;
    private readonly IFileSystemReaderService _fileSystemReaderService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetPatientsQueryHandler(
        IClientStorageSetting clientStorageSetting,
        IFileSystemReaderService fileSystemReaderService,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _clientStorageSetting = clientStorageSetting;
        _fileSystemReaderService = fileSystemReaderService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetPatientsResponse> Handle(
        GetPatientsQuery query,
        CancellationToken cancellationToken)
    {
        GetPatientsRequest request = query.GetPatientsRequest;
        var response = new GetPatientsResponse(request.CorrelationId);
        var specification = new ClientByIdSpecification(
            request.ClientId);

        var client = await _unitOfWork
            .ReadRepository<Client>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (client is null)
            throw new NotFoundException(nameof(client), request.ClientId);

        if (client.Patients is null)
            return response;

        response.Count = response.Patients.Count;

        response.Patients = await MapPatientsDtos(client.Patients);

        response.Count = response.Patients.Count;

        return response;
    }

    private async Task<List<PatientsDto>> MapPatientsDtos(
        IReadOnlyList<Patient> patients)
    {
        var patientsDtos = new List<PatientsDto>();

        foreach (Patient patient in patients)
        {
            string relativePhotoPath = Path.Combine(
                patient.ClientId.ToString(), patient.Photo.StoredName);

            string photoPath = Path.Combine(
                _clientStorageSetting.BasePath, relativePhotoPath);

            var patientsDto = new PatientsDto
            {
                ClientId = patient.ClientId,
                Name = patient.Name,
                PatientId = patient.Id,
                PhotoData = await _fileSystemReaderService.ReadAsync(photoPath),
                PhotoName = patient.Photo.Name,
                Sex = (int)patient.AnimalSex,
                Species = patient.AnimalType.Species
            };

            patientsDtos.Add(patientsDto);
        }

        return patientsDtos;
    }
}