using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatients;
using Fnunez.VeterinaryClinic.Scheduling.Application.Specifications;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.Queries.GetPatients;

public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, GetPatientsResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetPatientsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetPatientsResponse> Handle(
        GetPatientsQuery query,
        CancellationToken cancellationToken)
    {
        GetPatientsRequest request = query.GetPatientsRequest;
        var response = new GetPatientsResponse(request.CorrelationId);
        var specification = new ClientByIdIncludePatientsSpecification(request.ClientId);

        var client = await _unitOfWork.ReadRepository<Client>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (client is null)
            throw new NotFoundException(nameof(client), request.ClientId);

        if (client.Patients is null)
            return response;

        response.Patients = _mapper.Map<List<PatientDto>>(client.Patients);
        response.Count = response.Patients.Count;

        return response;
    }
}