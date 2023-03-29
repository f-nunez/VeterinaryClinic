using Fnunez.VeterinaryClinic.Public.Web.Helpers.SymmetricEncryption;
using Fnunez.VeterinaryClinic.Public.Web.ServiceBus;
using PublicContracts;

namespace Fnunez.VeterinaryClinic.Public.Web.Services.Appointment;

public class AppointmentService : IAppointmentService
{
    private readonly ILogger<AppointmentService> _logger;
    private readonly IServiceBus _serviceBus;
    private readonly ISymmetricEncryptionHelper _symmetricEncryptionHelper;

    public AppointmentService(
        ILogger<AppointmentService> logger,
        IServiceBus serviceBus,
        ISymmetricEncryptionHelper symmetricEncryptionHelper)
    {
        _logger = logger;
        _serviceBus = serviceBus;
        _symmetricEncryptionHelper = symmetricEncryptionHelper;
    }

    public async Task ConfirmAppointmentAsync(string encryptedAppointmentId)
    {
        _logger.LogInformation($"Encrypted Id: {encryptedAppointmentId}");

        string decryptedId = await _symmetricEncryptionHelper
            .DecryptFromBase64Async(encryptedAppointmentId);

        _logger.LogInformation($"Decrypted Id: {decryptedId}");

        var appointmentId = Guid.Parse(decryptedId);

        await SendIntegrationEventAsync(appointmentId);

        _logger.LogInformation($"Sent Id: {appointmentId.ToString()}");
    }

    private async Task SendIntegrationEventAsync(Guid appointmentId)
    {
        var correlationId = Guid.NewGuid();

        var contract = new AppointmentConfirmedIntegrationEventContract
        {
            AppointmentId = appointmentId,
            CausationId = correlationId,
            CorrelationId = correlationId,
            Id = correlationId,
            OccurredOn = DateTimeOffset.UtcNow
        };

        await _serviceBus.PublishAsync(contract);
    }
}