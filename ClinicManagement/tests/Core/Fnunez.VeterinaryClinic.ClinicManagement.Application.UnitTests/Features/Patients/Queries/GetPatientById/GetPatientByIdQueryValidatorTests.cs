using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientById;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Patients.Queries.GetPatientById;

public class GetPatientByIdQueryValidatorTests
{
    private readonly GetPatientByIdQueryValidator _validator;

    public GetPatientByIdQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_ClientIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int clientId = 1;

        var request = new GetPatientByIdRequest { ClientId = clientId };

        var query = new GetPatientByIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetPatientByIdRequest.ClientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_ClientIdIsLessThanOrEqualToZero_Fails(int clientId)
    {
        // Arrange=
        var request = new GetPatientByIdRequest { ClientId = clientId };

        var query = new GetPatientByIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetPatientByIdRequest.ClientId);
    }

    [Fact]
    public void Validation_PatientIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int patientId = 1;

        var request = new GetPatientByIdRequest { PatientId = patientId };

        var query = new GetPatientByIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetPatientByIdRequest.PatientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_PatientIdIsLessThanOrEqualToZero_Fails(int patientId)
    {
        // Arrange=
        var request = new GetPatientByIdRequest { PatientId = patientId };

        var query = new GetPatientByIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetPatientByIdRequest.PatientId);
    }
}