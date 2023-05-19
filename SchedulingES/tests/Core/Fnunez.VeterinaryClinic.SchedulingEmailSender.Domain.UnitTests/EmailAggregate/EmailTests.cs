using Fnunez.VeterinaryClinic.SchedulingEmailSender.Domain.EmailAggregate;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Domain.EmailAggregate.Enums;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Domain.UnitTests.EmailAggregate;

public class EmailTests
{
    private readonly string? _address = "test@nunez.ninja";
    private readonly Guid _correlationId = Guid.NewGuid();
    private readonly DateTimeOffset _createdOn = DateTimeOffset.UtcNow;
    private readonly EmailEvent _emailEvent = EmailEvent.AppointmentConfirmed;
    private readonly int _id = 1;
    private readonly string? _payload = "payload";
    private readonly string? _triggeredByUserId = Guid.NewGuid().ToString();

    [Fact]
    public void Constructor_Address_SetsAddressProperty()
    {
        // Arrange
        var email = new Email
        (
            _id,
            _correlationId,
            _address,
            _createdOn,
            _emailEvent,
            _payload,
            _triggeredByUserId
        );

        // Assert
        Assert.Equal(_address, email.Address);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_AddressIsEmpty_ThrowsArgumentException(
        string address)
    {
        // Arrange
        Action actual = () => new Email
        (
            _id,
            _correlationId,
            address,
            _createdOn,
            _emailEvent,
            _payload,
            _triggeredByUserId
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_CorrelationId_SetsCorrelationIdProperty()
    {
        // Arrange
        var email = new Email
        (
            _id,
            _correlationId,
            _address,
            _createdOn,
            _emailEvent,
            _payload,
            _triggeredByUserId
        );

        // Assert
        Assert.Equal(_correlationId, email.CorrelationId);
    }

    [Fact]
    public void Constructor_CorrelationIdIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var correlationId = Guid.Empty;

        Action actual = () => new Email
        (
            _id,
            correlationId,
            _address,
            _createdOn,
            _emailEvent,
            _payload,
            _triggeredByUserId
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_CreatedOn_SetsCreatedOnProperty()
    {
        // Arrange
        var email = new Email
        (
            _id,
            _correlationId,
            _address,
            _createdOn,
            _emailEvent,
            _payload,
            _triggeredByUserId
        );

        // Assert
        Assert.Equal(_createdOn, email.CreatedOn);
    }

    [Fact]
    public void Constructor_EmailEvent_SetsEmailEventProperty()
    {
        // Arrange
        var email = new Email
        (
            _id,
            _correlationId,
            _address,
            _createdOn,
            _emailEvent,
            _payload,
            _triggeredByUserId
        );

        // Assert
        Assert.Equal(_emailEvent, email.EmailEvent);
    }

    [Fact]
    public void Constructor_Id_SetsIdProperty()
    {
        // Arrange
        var email = new Email
        (
            _id,
            _correlationId,
            _address,
            _createdOn,
            _emailEvent,
            _payload,
            _triggeredByUserId
        );

        // Assert
        Assert.Equal(_id, email.Id);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_IdIsLessThanOrEqualToZero_ThrowsArgumentException(
        int id)
    {
        // Arrange
        Action actual = () => new Email
        (
            id,
            _correlationId,
            _address,
            _createdOn,
            _emailEvent,
            _payload,
            _triggeredByUserId
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_Payload_SetsPayloadProperty()
    {
        // Arrange
        var email = new Email
        (
            _id,
            _correlationId,
            _address,
            _createdOn,
            _emailEvent,
            _payload,
            _triggeredByUserId
        );

        // Assert
        Assert.Equal(_payload, email.Payload);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_PayloadIsEmpty_ThrowsArgumentException(
        string payload)
    {
        // Arrange
        var correlationId = Guid.Empty;

        Action actual = () => new Email
        (
            _id,
            correlationId,
            _address,
            _createdOn,
            _emailEvent,
            payload,
            _triggeredByUserId
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_TriggeredByUserId_SetsTriggeredByUserIdProperty()
    {
        // Arrange
        var email = new Email
        (
            _id,
            _correlationId,
            _address,
            _createdOn,
            _emailEvent,
            _payload,
            _triggeredByUserId
        );

        // Assert
        Assert.Equal(_triggeredByUserId, email.TriggeredByUserId);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_TriggeredByUserIdIsEmpty_ThrowsArgumentException(
        string triggeredByUserId)
    {
        // Arrange
        var correlationId = Guid.Empty;

        Action actual = () => new Email
        (
            _id,
            correlationId,
            _address,
            _createdOn,
            _emailEvent,
            _payload,
            triggeredByUserId
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void IncreaseRetryCount_IncreaseValueOnRetryCountProperty()
    {
        // Act
        var email = new Email
        (
            _id,
            _correlationId,
            _address,
            _createdOn,
            _emailEvent,
            _payload,
            _triggeredByUserId
        );

        var oneRetry = 1;

        // Act
        email.IncreaseRetryCount();

        // Assert
        Assert.Equal(oneRetry, email.RetryCount);
    }

    [Fact]
    public void UpdateSentOn_SentOn_UpdatesSentOnProperty()
    {
        // Act
        var email = new Email
        (
            _id,
            _correlationId,
            _address,
            _createdOn,
            _emailEvent,
            _payload,
            _triggeredByUserId
        );

        var sentOn = DateTimeOffset.UtcNow;

        // Act
        email.UpdateSentOn(sentOn);

        // Assert
        Assert.Equal(sentOn, email.SentOn);
    }
}