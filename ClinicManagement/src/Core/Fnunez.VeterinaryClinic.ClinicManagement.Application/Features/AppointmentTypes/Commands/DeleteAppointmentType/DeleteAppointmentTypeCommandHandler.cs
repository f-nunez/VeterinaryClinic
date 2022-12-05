using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.DeleteAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.DeleteAppointmentType;

public class DeleteAppointmentTypeCommandHandler : IRequestHandler<DeleteAppointmentTypeCommand, DeleteAppointmentTypeResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAppointmentTypeCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteAppointmentTypeResponse> Handle(
        DeleteAppointmentTypeCommand command,
        CancellationToken cancellationToken)
    {
        DeleteAppointmentTypeRequest request = command.DeleteAppointmentTypeRequest;
        var response = new DeleteAppointmentTypeResponse(request.CorrelationId);
        var appointmentTypeToDelete = _mapper.Map<AppointmentType>(request);

        await _unitOfWork.Repository<AppointmentType>()
            .DeleteAsync(appointmentTypeToDelete, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return response;
    }
}
