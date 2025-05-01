using CodeMate.Shared.Contracts;
using MassTransit;
using OrderService.Data;
using OrderService.Entities;
using OrderService.Events;

namespace OrderService.Consumers
{
    public class OfferApprovedConsumer : IConsumer<OfferApprovedEvent>
    {
        private readonly OrderDbContext _context;

        public OfferApprovedConsumer(OrderDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<OfferApprovedEvent> context)
        {
            var message = context.Message;

            var order = new Order
            {
                UserId = message.UserId,
                CourseId = message.CourseId,
                Amount = message.OfferedPrice,
                CreatedAt = DateTime.UtcNow,
                IsPaid = false
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            await context.Publish(new PaymentRequestedEvent
            {
                OrderId = order.Id,
                UserId = order.UserId,
                Amount = order.Amount
            });
        }

    }
}
