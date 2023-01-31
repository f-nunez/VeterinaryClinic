using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Interfaces.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Interfaces.Settings;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientEdit;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientsFilterPreferredDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientEdit;

public class GetPatientEditQueryHandler
    : IRequestHandler<GetPatientEditQuery, GetPatientEditResponse>
{
    private readonly IClientStorageSetting _clientStorageSetting;
    private readonly IFileSystemReaderService _fileSystemReaderService;
    private readonly IUnitOfWork _unitOfWork;

    public GetPatientEditQueryHandler(
        IClientStorageSetting clientStorageSetting,
        IFileSystemReaderService fileSystemReaderService,
        IUnitOfWork unitOfWork)
    {
        _clientStorageSetting = clientStorageSetting;
        _fileSystemReaderService = fileSystemReaderService;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetPatientEditResponse> Handle(
        GetPatientEditQuery query,
        CancellationToken cancellationToken)
    {
        GetPatientEditRequest request = query.GetPatientEditRequest;
        var response = new GetPatientEditResponse(request.CorrelationId);
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

        response.Patient = await MapPatientEditDtoAsync(patient);

        response.PreferredDoctorFilterValues = await MapPreferredDoctorFilterValuesAsync(
            patient, cancellationToken);

        return response;
    }

    private async Task<PatientEditDto> MapPatientEditDtoAsync(Patient patient)
    {
        string relativePhotoPath = Path.Combine(
            patient.ClientId.ToString(), patient.Photo.StoredName);

        string photoPath = Path.Combine(
            _clientStorageSetting.BasePath, relativePhotoPath);

        return new PatientEditDto
        {
            Breed = patient.AnimalType.Breed,
            ClientId = patient.ClientId,
            Name = patient.Name,
            PatientId = patient.Id,
            PhotoData = await _fileSystemReaderService.ReadAsync(photoPath),
            PhotoName = patient.Photo.Name,
            PreferredDoctorId = patient.PreferredDoctorId,
            Sex = (int)patient.AnimalSex,
            Species = patient.AnimalType.Species
        };
    }

    private async Task<List<PreferredDoctorFilterValueDto>> MapPreferredDoctorFilterValuesAsync(
        Patient patient,
        CancellationToken cancellationToken)
    {
        if (patient.PreferredDoctorId is null)
            return new List<PreferredDoctorFilterValueDto>();

        var preferredDoctor = await _unitOfWork
            .ReadRepository<Doctor>()
            .GetByIdAsync(patient.PreferredDoctorId.Value, cancellationToken);

        if (preferredDoctor is null)
            throw new NotFiniteNumberException(
                nameof(preferredDoctor), patient.PreferredDoctorId.Value);

        return new List<PreferredDoctorFilterValueDto>
        {
            new PreferredDoctorFilterValueDto
            {
                FullName = preferredDoctor.FullName,
                Id = preferredDoctor.Id
            }
        };
    }
}
