using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.CreateSchedule;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.DeleteSchedule;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.UpdateSchedule;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Mappings;

public class ScheduleProfile : Profile
{
    public ScheduleProfile()
    {
        CreateMap<Schedule, ScheduleDto>()
            .ForMember(
                dto => dto.ScheduleId,
                options => options.MapFrom(src => src.Id)
            ).ForPath(
                dto => dto.AppointmentIds,
                options => options.MapFrom(src => src.Appointments.Select(x => x.Id))
            );

        CreateMap<ScheduleDto, Schedule>()
            .ForMember(
                dto => dto.Id,
                options => options.MapFrom(src => src.ScheduleId)
            );

        CreateMap<CreateScheduleRequest, Schedule>();

        CreateMap<DeleteScheduleRequest, Schedule>();

        CreateMap<UpdateScheduleRequest, Schedule>()
            .ForMember(
                dto => dto.Id,
                options => options.MapFrom(src => src.ScheduleId)
            );
    }
}