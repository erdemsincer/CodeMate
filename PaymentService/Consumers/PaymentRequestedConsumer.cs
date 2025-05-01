using CodeMate.Shared.Contracts;
using MassTransit;

namespace PaymentService.Consumers;

public class PaymentRequestConsumer : IConsumer<PaymentRequestedEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public PaymentRequestConsumer(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<PaymentRequestedEvent> context)
    {
        var message = context.Message;

        Console.WriteLine($"💰 Payment received for OrderId: {message.OrderId}");

        // Simüle edilmiş ödeme (gerçek sistemde Stripe, iyzico vb. entegre edilir)
        await Task.Delay(1000);

        await _publishEndpoint.Publish(new PaymentCompletedEvent
        {
            OrderId = message.OrderId,
            IsSuccessful = true
        });

        Console.WriteLine($"✅ Payment completed published for OrderId: {message.OrderId}");
    }
}
