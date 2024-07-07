using MediatR;
using Microsoft.EntityFrameworkCore;
using RFID.SimpleTask.Application.Common.Interfaces;
using RFID.SimpleTask.Domain.Entities;

namespace RFID.SimpleTask.Application.Orders.Queries.Search;

public class SearchOrderQuery : IRequest<IEnumerable<Order>>
{
    public string SearchTerm { get; set; } = null!;
}

public class SearchOrderQueryHandler : IRequestHandler<SearchOrderQuery, IEnumerable<Order>>
{
    private readonly IApplicationDbContext _context;

    public SearchOrderQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> Handle(SearchOrderQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Orders.AsQueryable();

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            query = query.Where(o => o.User.Name.Contains(request.SearchTerm) ||
                                     o.OrderItems.Any(oi => oi.ProductName.Contains(request.SearchTerm)));
        }

        var orders = await query
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

        return orders;
    }
}
