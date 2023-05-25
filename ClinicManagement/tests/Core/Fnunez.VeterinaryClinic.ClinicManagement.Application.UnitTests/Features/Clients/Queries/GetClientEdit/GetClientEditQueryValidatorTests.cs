using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientEdit;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientEdit;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Clients.Queries.GetClientEdit;

public class GetClientEditQueryValidatorTests
{
    private readonly GetClientEditQueryValidator _validator;

    public GetClientEditQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_ClientIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int clientId = 1;

        var request = new GetClientEditRequest { ClientId = clientId };

        var query = new GetClientEditQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetClientEditRequest.ClientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_ClientIdIsLessThanOrEqualToZero_Fails(int clientId)
    {
        // Arrange
        var request = new GetClientEditRequest { ClientId = clientId };

        var query = new GetClientEditQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientEditRequest.ClientId);
    }
}