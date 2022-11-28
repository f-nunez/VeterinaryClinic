using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.CreatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientById;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientById;

public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, GetPatientByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetPatientByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetPatientByIdResponse> Handle(
        GetPatientByIdQuery query,
        CancellationToken cancellationToken)
    {
        GetPatientByIdRequest request = query.GetPatientByIdRequest;
        var response = new GetPatientByIdResponse(request.CorrelationId);
        var specification = new ClientByIdIncludePatientsSpecification(request.ClientId);

        var client = await _unitOfWork.ReadRepository<Client>()
            .FirstOrDefaultAsync(specification, cancellationToken);

        if (client is null)
            return response;

        var patient = client.Patients
            .FirstOrDefault(p => p.Id == request.PatientId);

        if (patient is null)
            return response;

        response.Patient = _mapper.Map<PatientDto>(patient);

        return response;
    }
}
