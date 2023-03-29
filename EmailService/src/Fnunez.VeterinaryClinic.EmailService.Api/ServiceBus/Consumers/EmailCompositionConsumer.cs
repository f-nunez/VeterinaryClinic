using System.Text.Json;
using Fnunez.VeterinaryClinic.EmailService.Api.Services.Email;
using MassTransit;
using SchedulingEmailSenderContracts;

namespace Fnunez.VeterinaryClinic.EmailService.Api.ServiceBus.Consumers;
public class EmailCompositionConsumer : IConsumer<EmailCompositionContract>
{
    private readonly IEmailService _emailService;

    public EmailCompositionConsumer(IEmailService emailService)
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