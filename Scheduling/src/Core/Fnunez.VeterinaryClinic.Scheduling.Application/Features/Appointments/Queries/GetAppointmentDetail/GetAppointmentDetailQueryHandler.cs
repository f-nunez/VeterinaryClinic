using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterAppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterDoctor;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterRoom;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.DoctorAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentDetail;

public class GetAppointmentDetailQueryHandler
    : IRequestHandler<GetAppointmentDetailQuery, GetAppointmentDetailResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentDetailQueryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
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

        response.Appointment = _mapper.Map<AppointmentDto>(appointment);

        response.AppointmentTypeFilterValues = MapAppointemntTypeFilterValues(
            appointment.AppointmentType);

        response.DoctorFilterValues = MapDoctorFilterValues(appointment.Doctor);

        response.RoomFilterValues = MapRoomFilterValues(appointment.Room);

        return response;
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