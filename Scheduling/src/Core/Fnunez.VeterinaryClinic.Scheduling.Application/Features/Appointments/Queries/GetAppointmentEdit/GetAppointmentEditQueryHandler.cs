using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Interfaces.Services;
using Fnunez.VeterinaryClinic.Scheduling.Application.Interfaces.Settings;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentEdit;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterAppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterDoctor;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterRoom;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.DoctorAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentEdit;

public class GetAppointmentEditQueryHandler
    : IRequestHandler<GetAppointmentEditQuery, GetAppointmentEditResponse>
{
    private readonly IClientStorageSetting _clientStorageSetting;
    private readonly IFileSystemReaderService _fileSystemReaderService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentEditQueryHandler(
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

    public async Task<GetAppointmentEditResponse> Handle(
        GetAppointmentEditQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentEditRequest request = query
            .GetAppointmentEditRequest;

        var response = new GetAppointmentEditResponse(
            request.AppointmentId);

        var specification = new AppointmentByIdSpecification(
            request.AppointmentId);

        var appointment = await _unitOfWork
            .ReadRepository<Appointment>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (appointment is null)
            throw new NotFoundException(
                nameof(appointment), request.AppointmentId);

        var appointmentEditDto = _mapper.Map<AppointmentEditDto>(appointment);

        appointmentEditDto.PatientPhotoData = await GetPatientPhotoDataAsync(
            appointment.Patient);

        response.Appointment = appointmentEditDto;

        response.AppointmentTypeFilterValues = MapAppointemntTypeFilterValues(
            appointment.AppointmentType);

        response.DoctorFilterValues = MapDoctorFilterValues(appointment.Doctor);

        response.RoomFilterValues = MapRoomFilterValues(appointment.Room);

        return response;
    }

    private async Task<byte[]> GetPatientPhotoDataAsync(Patient patient)
    {
        string relativePhotoPath = Path.Combine(
            patient.ClientId.ToString(), patient.Photo.StoredName);

        string photoPath = Path.Combine(
            _clientStorageSetting.BasePath, relativePhotoPath);

        return await _fileSystemReaderService.ReadAsync(photoPath);
    }

    private List<AppointmentTypeFilterValueDto> MapAppointemntTypeFilterValues(
        AppointmentType appointmentType)
    {
        return new List<AppointmentTypeFilterValueDto>
        {
            new AppointmentTypeFilterValueDto
            {
                Code = appointmentType.Code,
                Duration = appointmentType.Duration,
                Id = appointmentType.Id,
                Name = appointmentType.Name
            }
        };
    }

    private List<DoctorFilterValueDto> MapDoctorFilterValues(Doctor doctor)
    {
        return new List<DoctorFilterValueDto>
        {
            new DoctorFilterValueDto
            {
                FullName = doctor.FullName,
                Id = doctor.Id
            }
        };
    }

    private List<RoomFilterValueDto> MapRoomFilterValues(Room room)
    {
        return new List<RoomFilterValueDto>
        {
            new RoomFilterValueDto
            {
                Id = room.Id,
                Name = room.Name
            }
        };
    }
}