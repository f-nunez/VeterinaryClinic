using System.Text.Json;
using Contracts.SchedulingEmailSender;
using Fnunez.VeterinaryClinic.EmailService.Api.Services.Email;
using MassTransit;

namespace Fnunez.VeterinaryClinic.EmailService.Api.ServiceBus.Consumers;
public class EmailCompositionSchedulingEmailSenderConsumer
    : IConsumer<EmailCompositionSchedulingEmailSenderContract>
{
    private readonly IEmailService _emailService;

    public EmailCompositionSchedulingEmailSenderConsumer(
        IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Consume(
        ConsumeContext<EmailCompositionSchedulingEmailSenderContract> context)
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