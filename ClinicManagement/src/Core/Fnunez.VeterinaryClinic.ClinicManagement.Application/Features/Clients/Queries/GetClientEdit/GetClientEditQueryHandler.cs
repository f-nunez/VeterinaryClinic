using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientEdit;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterPreferredDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientEdit;

public class GetClientEditQueryHandler
    : IRequestHandler<GetClientEditQuery, GetClientEditResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetClientEditQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClientEditResponse> Handle(
        GetClientEditQuery query,
        CancellationToken cancellationToken)
    {
        GetClientEditRequest request = query.GetClientEditRequest;
        var response = new GetClientEditResponse(request.CorrelationId);
        var specification = new GetClientByIdSpecification(request.ClientId);

        var client = await _unitOfWork
            .ReadRepository<Client>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (client is null)
            throw new NotFoundException(nameof(client), request.ClientId);

        var preferredDoctorFilterValues = new List<PreferredDoctorFilterValueDto>
        {
            new PreferredDoctorFilterValueDto
            {
                FullName = client.PreferredDoctor.FullName,
                Id = client.PreferredDoctor.Id
            }
        };

        response.Client = _mapper.Map<ClientDto>(client);
        response.PreferredDoctorFilterValues = preferredDoctorFilterValues;

        return response;
    }
}