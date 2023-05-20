using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentAdd;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentAdd;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Features.Appointments.Queries.GetAppointmentAdd;

public class GetAppointmentAddQueryValidatorTests
{
    private readonly GetAppointmentAddQueryValidator _validator;

    public GetAppointmentAddQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_ClientIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int clientId = 1;

        var request = new GetAppointmentAddRequest
        {
            ClientId = clientId
        };

        var query = new GetAppointmentAddQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetAppointmentAddRequest.ClientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_ClientIdIsLessThanOrEqualToZero_Fails(int clientId)
    {
        // Arrange
        var request = new GetAppointmentAddRequest
        {
            ClientId = clientId
        };

        var query = new GetAppointmentAddQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentAddRequest.ClientId);
    }

    [Fact]
    public void Validation_ClinicIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int clinicId = 1;

        var request = new GetAppointmentAddRequest
        {
            ClinicId = clinicId
        };

        var query = new GetAppointmentAddQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetAppointmentAddRequest.ClinicId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_ClinicIdIsLessThanOrEqualToZero_Fails(int clinicId)
    {
        // Arrange
        var request = new GetAppointmentAddRequest
        {
            ClinicId = clinicId
        };

        var query = new GetAppointmentAddQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentAddRequest.ClinicId);
    }

    [Fact]
    public void Validation_PatientIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int patientId = 1;

        var request = new GetAppointmentAddRequest
        {
            PatientId = patientId
        };

        var query = new GetAppointmentAddQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetAppointmentAddRequest.PatientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_PatientIdIsLessThanOrEqualToZero_Fails(
        int patientId)
    {
        // Arrange
        var request = new GetAppointmentAddRequest
        {
            PatientId = patientId
        };

        var query = new GetAppointmentAddQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentAddRequest.PatientId);
    }
}