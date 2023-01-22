using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicById;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicById;

public class GetClinicByIdQueryHandler : IRequestHandler<GetClinicByIdQuery, GetClinicByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetClinicByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClinicByIdResponse> Handle(
        GetClinicByIdQuery query,
        CancellationToken cancellationToken)
    {
        GetClinicByIdRequest request = query.GetClinicByIdRequest;
        var response = new GetClinicByIdResponse(request.CorrelationId);

        var clinic = await _unitOfWork
            .ReadRepository<Clinic>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (clinic is null)
            throw new NotFoundException(nameof(clinic), request.Id);

        response.Clinic = _mapper.Map<ClinicDto>(clinic);

        return response;
    }
}