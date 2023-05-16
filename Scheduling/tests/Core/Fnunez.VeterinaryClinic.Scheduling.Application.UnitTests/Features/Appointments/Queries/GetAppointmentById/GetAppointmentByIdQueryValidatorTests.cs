using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentById;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentById;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Features.Appointments.Queries.GetAppointmentById;

public class GetAppointmentByIdQueryValidatorTests
{
    private readonly GetAppointmentByIdQueryValidator _validator;

    public GetAppointmentByIdQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_AppointmentId_IsValid()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();

        var request = new GetAppointmentByIdRequest
        {
            AppointmentId = appointmentId
        };

        var query = new GetAppointmentByIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetAppointmentByIdRequest.AppointmentId);
    }

    [Fact]
    public void Validation_AppointmentIdIsEmpty_Fails()
    {
        // Arrange
        var appointmentId = Guid.Empty;

        var request = new GetAppointmentByIdRequest
        {
            AppointmentId = appointmentId
        };

        var query = new GetAppointmentByIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentByIdRequest.AppointmentId);
    }
}