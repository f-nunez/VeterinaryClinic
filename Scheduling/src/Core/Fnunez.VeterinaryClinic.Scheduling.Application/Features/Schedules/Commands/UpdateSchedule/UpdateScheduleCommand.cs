using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.UpdateSchedule;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Schedules.Commands.UpdateSchedule;

public record UpdateScheduleCommand(UpdateScheduleRequest UpdateScheduleRequest) : IRequest<UpdateScheduleResponse>;