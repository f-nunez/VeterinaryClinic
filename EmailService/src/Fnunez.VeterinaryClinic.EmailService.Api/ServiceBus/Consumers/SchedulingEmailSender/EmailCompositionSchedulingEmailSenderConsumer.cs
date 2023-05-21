using System.Text.Json;
using Contracts.SchedulingEmailSender;
using Fnunez.VeterinaryClinic.EmailService.Api.Services.Email;
using MassTransit;

namespace Fnunez.VeterinaryClinic.EmailService.Api.ServiceBus.Consumers;
public class EmailCompositionSchedulingemailSenderConsumer
    : IConsumer<EmailCompositionContract>
{
    private readonly IEmailService _emailService;

    public EmailCompositionSchedulingemailSenderConsumer(
        IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Consume(ConsumeContext<EmailCompositionContract> context)
    {
        var contract = context.Message;

        var emailComposition = JsonSerializer
            .Deserialize<EmailComposition>(contract.SerializedEmailComposition);

        if (emailComposition is null)
            throw new NullReferenceException($"{nameof(emailComposition)}");

        _emailService.SendEmail(emailComposition);

        await Task.CompletedTask;
    }
}