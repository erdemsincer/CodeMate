using CodeMate.Shared.Contracts;
using MassTransit;
using OrderService.Data;

namespace OrderService.Consumers
{
    public class PaymentCompletedConsumer : IConsumer<PaymentCompletedEvent>
    {
        private readonly OrderDbContext _context;

        public PaymentCompletedConsumer(OrderDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            var data = context.Message;

            Console.WriteLine($"✅ PaymentCompletedEvent alındı. OrderId: {data.OrderId}");

            var order = await _context.Orders.FindAsync(data.OrderId);
            if (order == null)
            {
                Console.WriteLine($"⚠️ Order bulunamadı: {data.OrderId}");
                return;
            }

            if (data.IsSuccessful)
            {
                order.IsPaid = true;
                await _context.SaveChangesAsync();

                Console.WriteLine($"💰 Order işaretlendi: {order.Id} -> IsPaid = true");
            }
        }
    }
}
