using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentEdit;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentEdit;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Features.Appointments.Queries.GetAppointmentEdit;

public class GetAppointmentEditQueryValidatorTests
{
    private readonly GetAppointmentEditQueryValidator _validator;

    public GetAppointmentEditQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_AppointmentId_IsValid()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();

        var request = new GetAppointmentEditRequest
        {
            AppointmentId = appointmentId
        };

        var query = new GetAppointmentEditQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetAppointmentEditRequest.AppointmentId);
    }

    [Fact]
    public void Validation_AppointmentIdIsEmpty_Fails()
    {
        // Arrange
        var appointmentId = Guid.Empty;

        var request = new GetAppointmentEditRequest
        {
            AppointmentId = appointmentId
        };

        var query = new GetAppointmentEditQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentEditRequest.AppointmentId);
    }
}