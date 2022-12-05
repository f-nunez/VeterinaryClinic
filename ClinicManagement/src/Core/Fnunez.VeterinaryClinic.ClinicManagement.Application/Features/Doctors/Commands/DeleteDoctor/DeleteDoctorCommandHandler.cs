using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.DeleteDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.DeleteDoctor;

public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, DeleteDoctorResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDoctorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteDoctorResponse> Handle(
        DeleteDoctorCommand command,
        CancellationToken cancellationToken)
    {
        DeleteDoctorRequest request = command.DeleteDoctorRequest;
        var response = new DeleteDoctorResponse(request.CorrelationId);
        var doctorToDelete = _mapper.Map<Doctor>(request);

        await _unitOfWork.Repository<Doctor>()
            .DeleteAsync(doctorToDelete, cancellationToken);
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return response;
    }
}
