using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services;
using Fnunez.VeterinaryClinic.Scheduling.Application.Settings;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentDetail;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentDetail;

public class GetAppointmentDetailQueryHandler
    : IRequestHandler<GetAppointmentDetailQuery, GetAppointmentDetailResponse>
{
    private readonly IClientStorageSetting _clientStorageSetting;
    private readonly IFileSystemReaderService _fileSystemReaderService;
    private readonly ILogger<GetAppointmentDetailQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentDetailQueryHandler(
        IClientStorageSetting clientStorageSetting,
        IFileSystemReaderService fileSystemReaderService,
        ILogger<GetAppointmentDetailQueryHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _clientStorageSetting = clientStorageSetting;
        _fileSystemReaderService = fileSystemReaderService;
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentDetailResponse> Handle(
        GetAppointmentDetailQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentDetailRequest request = query
            .GetAppointmentDetailRequest;

        var response = new GetAppointmentDetailResponse(
            request.AppointmentId);

        var specification = new AppointmentByIdSpecification(
            request.AppointmentId);

        var appointment = await _unitOfWork
            .ReadRepository<Appointment>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (appointment is null)
            throw new NotFoundException(
                nameof(appointment),
                request.AppointmentId
            );

        var appointmentDetail = _mapper.Map<AppointmentDetailDto>(appointment);

        appointmentDetail.PatientPhotoData = await GetPatientPhotoDataAsync(
            appointment.Patient);

        response.Appointment = appointmentDetail;

        return response;
    }

    private async Task<byte[]?> GetPatientPhotoDataAsync(Patient patient)
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