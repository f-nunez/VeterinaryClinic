using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.DeleteClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Commands.DeleteClinic;

public class DeleteClinicCommandHandler : IRequestHandler<DeleteClinicCommand, DeleteClinicResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClinicCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteClinicResponse> Handle(
        DeleteClinicCommand command,
        CancellationToken cancellationToken)
    {
        DeleteClinicRequest request = command.DeleteClinicRequest;
        var response = new DeleteClinicResponse(request.CorrelationId);
        var clinicToDelete = _mapper.Map<Clinic>(request);

        await _unitOfWork
            .Repository<Clinic>()
            .DeleteAsync(clinicToDelete, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return response;
    }
}