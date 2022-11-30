using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.UpdateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.UpdateAppointmentType;

public class UpdateAppointmentTypeCommandHandler : IRequestHandler<UpdateAppointmentTypeCommand, UpdateAppointmentTypeResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAppointmentTypeCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateAppointmentTypeResponse> Handle(
        UpdateAppointmentTypeCommand command,
        CancellationToken cancellationToken)
    {
        UpdateAppointmentTypeRequest request = command.UpdateAppointmentTypeRequest;
        var response = new UpdateAppointmentTypeResponse(request.CorrelationId);
        var appointmentTypeToUpdate = _mapper.Map<AppointmentType>(request);

        await _unitOfWork.Repository<AppointmentType>()
            .UpdateAsync(appointmentTypeToUpdate, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        response.AppointmentType = _mapper
            .Map<AppointmentTypeDto>(appointmentTypeToUpdate);

        return response;
    }
}
