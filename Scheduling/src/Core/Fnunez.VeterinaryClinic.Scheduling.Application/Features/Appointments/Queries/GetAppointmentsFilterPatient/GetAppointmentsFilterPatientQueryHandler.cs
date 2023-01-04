using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterPatient;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterPatient;

public class GetAppointmentsFilterPatientQueryHandler
    : IRequestHandler<GetAppointmentsFilterPatientQuery, GetAppointmentsFilterPatientResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentsFilterPatientQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentsFilterPatientResponse> Handle(
        GetAppointmentsFilterPatientQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentsFilterPatientRequest request = query
            .GetAppointmentsFilterPatientRequest;

        var response = new GetAppointmentsFilterPatientResponse(
            request.CorrelationId);

        var specification = new ClientByIdIncludePatientsSpecification(
            request.ClientId);

        var client = await _unitOfWork
            .ReadRepository<Client>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (client is null)
            throw new NotFoundException(nameof(client), request.ClientId);

        if (client.Patients is null)
            return response;

        var patientFilterValues = _mapper
            .Map<List<PatientFilterValueDto>>(client.Patients);

        response.PatientFilterValues = patientFilterValues
            .OrderBy(p => p.Name).ToList();

        return response;
    }
}