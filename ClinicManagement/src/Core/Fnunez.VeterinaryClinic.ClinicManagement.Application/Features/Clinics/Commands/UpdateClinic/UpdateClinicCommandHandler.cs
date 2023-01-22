using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.UpdateClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Commands.UpdateClinic;

public class UpdateClinicCommandHandler : IRequestHandler<UpdateClinicCommand, UpdateClinicResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClinicCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateClinicResponse> Handle(
        UpdateClinicCommand command,
        CancellationToken cancellationToken)
    {
        UpdateClinicRequest request = command.UpdateClinicRequest;
        var response = new UpdateClinicResponse(request.CorrelationId);
        var clinicToUpdate = _mapper.Map<Clinic>(request);

        await _unitOfWork
            .Repository<Clinic>()
            .UpdateAsync(clinicToUpdate, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        response.Clinic = _mapper.Map<ClinicDto>(clinicToUpdate);

        return response;
    }
}