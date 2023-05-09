using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Clients.Queries.GetClients;

public class GetClientsQueryValidatorTests
{
    private readonly GetClientsQueryValidator _validator;

    public GetClientsQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_DataGridRequestSearchHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var search = string.Empty;

        for (int i = 0; i < 201; i++)
            search += "a";

        var dataGridRequest = new DataGridRequest { Search = search };

        var request = new GetClientsRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetClientsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientsRequest.DataGridRequest.Search);
    }

    [Fact]
    public void Validation_DataGridRequestSkipIsLessThanZero_Fails()
    {
        // Arrange
        int skip = -1;

        var dataGridRequest = new DataGridRequest { Skip = skip };

        var request = new GetClientsRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetClientsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientsRequest.DataGridRequest.Skip);
    }

    [Fact]
    public void Validation_DataGridRequestTakeIsGreaterThanOnehundred_Fails()
    {
        // Arrange
        int take = 101;

        var dataGridRequest = new DataGridRequest { Take = take };

        var request = new GetClientsRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetClientsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientsRequest.DataGridRequest.Take);
    }

    [Fact]
    public void Validation_DataGridRequestTakeIsLessThanZero_Fails()
    {
        // Arrange
        int take = -1;

        var dataGridRequest = new DataGridRequest { Take = take };

        var request = new GetClientsRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetClientsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientsRequest.DataGridRequest.Take);
    }

    [Fact]
    public void Validation_EmailAddressFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var emailAddressFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            emailAddressFilterValue += "a";

        var request = new GetClientsRequest
        {
            EmailAddressFilterValue = emailAddressFilterValue
        };

        var query = new GetClientsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientsRequest.EmailAddressFilterValue);
    }

    [Fact]
    public void Validation_FullNameFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var fullNameFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            fullNameFilterValue += "a";

        var request = new GetClientsRequest
        {
            FullNameFilterValue = fullNameFilterValue
        };

        var query = new GetClientsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientsRequest.FullNameFilterValue);
    }

    [Fact]
    public void Validation_IdFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var idFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            idFilterValue += "a";

        var request = new GetClientsRequest
        {
            IdFilterValue = idFilterValue
        };

        var query = new GetClientsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientsRequest.IdFilterValue);
    }

    [Fact]
    public void Validation_PreferredNameFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var preferredNameFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            preferredNameFilterValue += "a";

        var request = new GetClientsRequest
        {
            PreferredNameFilterValue = preferredNameFilterValue
        };

        var query = new GetClientsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientsRequest.PreferredNameFilterValue);
    }

    [Fact]
    public void Validation_SalutationFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var salutationFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            salutationFilterValue += "a";

        var request = new GetClientsRequest
        {
            SalutationFilterValue = salutationFilterValue
        };

        var query = new GetClientsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientsRequest.SalutationFilterValue);
    }
}