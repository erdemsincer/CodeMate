using CodeMate.Shared.Contracts;
using MassTransit;
using NotificationService.Services;

namespace NotificationService.Consumers
{
    public class PaymentCompletedConsumer : IConsumer<PaymentCompletedEvent>
    {
        private readonly IEmailService _emailService;

        public PaymentCompletedConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            var message = context.Message;

            Console.WriteLine($"[Notification] Payment successful for OrderId: {message.OrderId}");

            await _emailService.SendEmailAsync(
                to: "erdemsincer@gmail.com", // Test adresi
                subject: "Ödeme Başarılı",
                body: $@"
                    Merhaba,<br><br>
                    1000₺ tutarındaki ödemeniz başarıyla alınmıştır.<br>
                    Sipariş Numaranız: <strong>{message.OrderId}</strong><br><br>
                    Teşekkür ederiz.<br>
                    <em>CodeMate Ekibi</em>"
            );
        }
    }
}
