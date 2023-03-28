using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine;
using MassTransit;
using SchedulingContracts;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Infrastructure.ServiceBus.Consumers;

public class EmailRequestConsumer : IConsumer<EmailRequestContract>
{
    private readonly IEmailEngineService _emailEngineService;

    public EmailRequestConsumer(IEmailEngineService emailEngineService)
    {
        _emailEngineService = emailEngineService;
    }

    public async Task Consume(ConsumeContext<EmailRequestContract> context)
    {
        await _emailEngineService.CreateAndSendAsync(
            context.Message.EmailEvent,
            context.Message.SerializedEmailRequest,
            context.CancellationToken
        );
    }
}