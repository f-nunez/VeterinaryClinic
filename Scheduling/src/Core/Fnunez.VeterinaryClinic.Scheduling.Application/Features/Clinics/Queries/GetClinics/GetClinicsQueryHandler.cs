using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinics;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinics;

public class GetClinicsQueryHandler
    : IRequestHandler<GetClinicsQuery, GetClinicsResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetClinicsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClinicsResponse> Handle(
        GetClinicsQuery query,
        CancellationToken cancellationToken)
    {
        GetClinicsRequest request = query.GetClinicsRequest;
        var response = new GetClinicsResponse(request.CorrelationId);
        var specification = new ClinicsSpecification(request);
        var countSpecification = new ClinicsCountSpecification(request);

        var clinics = await _unitOfWork
            .ReadRepository<Clinic>()
            .ListAsync(specification, cancellationToken);

        int count = await _unitOfWork
            .ReadRepository<Clinic>()
            .CountAsync(countSpecification, cancellationToken);

        if (clinics is null)
            return response;

        var clinicDtos = _mapper.Map<List<ClinicDto>>(clinics);

        response.DataGridResponse = new DataGridResponse<ClinicDto>(
            clinicDtos,
            count
        );

        return response;
    }
}