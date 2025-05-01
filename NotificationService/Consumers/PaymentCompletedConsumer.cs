using CodeMate.Shared.Contracts;
using MassTransit;

namespace NotificationService.Consumers
{
    public class PaymentCompletedConsumer : IConsumer<PaymentCompletedEvent>
    {
        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            var message = context.Message;

            // Burada mail atabilir ya da loglayabilirsin.
            Console.WriteLine($"[Notification] Payment successful for OrderId: {message.OrderId}");

            await Task.CompletedTask;
        }
    }
}
