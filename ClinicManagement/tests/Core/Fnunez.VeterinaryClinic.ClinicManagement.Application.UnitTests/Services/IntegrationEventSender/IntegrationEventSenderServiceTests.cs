using Contracts.ClinicManagement;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.IntegrationEventSender.Factories;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Services.IntegrationEventSender;

public class IntegrationEventSenderServiceTests
{
    private readonly string _appointmentTypeCode = "c";
    private readonly int _appointmentTypeDuration = 1;
    private readonly int _appointmentTypeId = 1;
    private readonly string _appointmentTypeName = "n";
    private readonly Guid _correlationId = Guid.NewGuid();
    private readonly AppointmentTypeCreatedIntegrationEventFactory _factory;

    public IntegrationEventSenderServiceTests()
    {
        var appointmentType = new AppointmentType
        (
            _appointmentTypeId,
            _appointmentTypeName,
            _appointmentTypeCode,
            _appointmentTypeDuration
        );

        _factory = new AppointmentTypeCreatedIntegrationEventFactory
        (
            appointmentType
        );
    }

    [Fact]
    public async void SendAsync_CallsPublishAsyncMethodOnceFromServiceBus()
    {
        // Arrange
        var mockIServiceBus = new Mock<IServiceBus>();

        mockIServiceBus.Setup(x =>
            x.PublishAsync(
                It.IsAny<IntegrationEventClinicManagementContract>(),
                CancellationToken.None
            )
        );

        var integrationEventSenderService = new IntegrationEventSenderService(
            mockIServiceBus.Object);

        // Act
        await integrationEventSenderService.SendAsync(
            _factory, _correlationId, CancellationToken.None);

        // Assert
        mockIServiceBus.Verify(x =>
            x.PublishAsync(
                It.IsAny<IntegrationEventClinicManagementContract>(),
                CancellationToken.None
            ),
            Times.Once()
        );
    }
}