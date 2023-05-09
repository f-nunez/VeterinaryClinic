using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.DeletePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.DeletePatient;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Patients.Commands.DeletePatient;

public class DeletePatientCommandValidatorTests
{
    private readonly DeletePatientCommandValidator _validator;

    public DeletePatientCommandValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_ClientIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int clientId = 1;

        var request = new DeletePatientRequest { ClientId = clientId };

        var command = new DeletePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.DeletePatientRequest.ClientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_ClientIdIsLessOrEqualsThanZero_Fails(int clientId)
    {
        // Arrange
        var request = new DeletePatientRequest { ClientId = clientId };

        var command = new DeletePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.DeletePatientRequest.ClientId);
    }

    [Fact]
    public void Validation_PatientIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int patientId = 1;

        var request = new DeletePatientRequest { PatientId = patientId };

        var command = new DeletePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.DeletePatientRequest.PatientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_PatientIdIsLessOrEqualsThanZero_Fails(int patientId)
    {
        // Arrange
        var request = new DeletePatientRequest { PatientId = patientId };

        var command = new DeletePatientCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.DeletePatientRequest.PatientId);
    }
}