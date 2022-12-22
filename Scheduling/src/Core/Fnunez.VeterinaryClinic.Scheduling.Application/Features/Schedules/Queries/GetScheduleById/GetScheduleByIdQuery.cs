using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.GetScheduleById;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Schedules.Queries.GetScheduleById;

public record GetScheduleByIdQuery(GetScheduleByIdRequest GetScheduleByIdRequest)
    : IRequest<GetScheduleByIdResponse>;