using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.CreateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.CreateAppointmentType;

public class CreateAppointmentTypeCommandHandler : IRequestHandler<CreateAppointmentTypeCommand, CreateAppointmentTypeResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAppointmentTypeCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateAppointmentTypeResponse> Handle(
        CreateAppointmentTypeCommand command,
        CancellationToken cancellationToken)
    {
        CreateAppointmentTypeRequest request = command.CreateAppointmentTypeRequest;
        var response = new CreateAppointmentTypeResponse(request.CorrelationId);
        var newAppointemntType = _mapper.Map<AppointmentType>(request);

        newAppointemntType = await _unitOfWork
            .Repository<AppointmentType>()
            .AddAsync(newAppointemntType, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        response.AppointmentType = _mapper
            .Map<AppointmentTypeDto>(newAppointemntType);

        return response;
    }
}
