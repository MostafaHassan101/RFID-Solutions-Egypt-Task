using MediatR;
using RFID.SimpleTask.Application.Common.Interfaces;
using RFID.SimpleTask.Domain.Entities;

namespace RFID.SimpleTask.Application.Orders.Commands.Create;
public class CreateOrderCommand : IRequest<int>
{
    public string CustomerId { get; set; } = null!;
    public List<OrderItem> OrderItems { get; set; } = null!;
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            UserId = request.CustomerId,
            Created = DateTime.UtcNow,
            OrderItems = request.OrderItems.Select(item => new OrderItem
            {
                ProductName = item.ProductName,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList()
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}