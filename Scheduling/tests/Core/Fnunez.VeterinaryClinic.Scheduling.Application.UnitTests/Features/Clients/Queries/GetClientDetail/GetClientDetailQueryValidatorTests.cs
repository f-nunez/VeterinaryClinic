using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientDetail;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Features.Clients.Queries.GetClientDetail;

public class GetClientDetailQueryValidatorTests
{
    private readonly GetClientDetailQueryValidator _validator;

    public GetClientDetailQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_ClientIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int clientId = 1;

        var request = new GetClientDetailRequest { ClientId = clientId };

        var query = new GetClientDetailQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetClientDetailRequest.ClientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_ClientIdIsLessThanOrEqualToZero_Fails(int clientId)
    {
        // Arrange
        var request = new GetClientDetailRequest { ClientId = clientId };

        var query = new GetClientDetailQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientDetailRequest.ClientId);
    }
}