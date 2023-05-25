using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.Queries.GetPatients;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatients;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Features.Patients.Queries.GetPatients;

public class GetPatientsQueryValidatorTests
{
    private readonly GetPatientsQueryValidator _validator;

    public GetPatientsQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_ClientIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int clientId = 1;

        var request = new GetPatientsRequest { ClientId = clientId };

        var query = new GetPatientsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetPatientsRequest.ClientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_ClientIdIsLessThanOrEqualToZero_Fails(int clientId)
    {
        // Arrange=
        var request = new GetPatientsRequest { ClientId = clientId };

        var query = new GetPatientsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetPatientsRequest.ClientId);
    }
}