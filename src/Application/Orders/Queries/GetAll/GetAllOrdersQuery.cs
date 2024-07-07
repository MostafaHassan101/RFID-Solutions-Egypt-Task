using MediatR;
using Microsoft.EntityFrameworkCore;
using RFID.SimpleTask.Application.Common.Interfaces;
using RFID.SimpleTask.Domain.Entities;

namespace RFID.SimpleTask.Application.Orders.Queries.GetAll;

public record GetAllOrdersQuery : IRequest<IEnumerable<Order>>
{
}

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<Order>>
{
	private readonly IApplicationDbContext _context;

    public GetAllOrdersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
	{
        return await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
            .Select(o => new Order
            {
                Id = o.Id,
                UserId = o.UserId,
                //Name = o.User.Name,
                Created = o.Created,
                OrderItems = o.OrderItems.Select(oi => new OrderItem
                {
                    Id = oi.Id,
                    ProductName = oi.ProductName,
                    Price = oi.Price,
                    Quantity = oi.Quantity
                }).ToList()
            })
            .ToListAsync(cancellationToken);
	}
}