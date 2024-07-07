using MediatR;
using Microsoft.EntityFrameworkCore;
using RFID.SimpleTask.Application.Common.Interfaces;
using RFID.SimpleTask.Domain.Entities;

namespace RFID.SimpleTask.Application.Orders.Queries.GetForCurrentUser;


public record GetOrdersForCurrentUser : IRequest<IEnumerable<Order>>
{
}

public class GetOrdersForCurrentUserHandler : IRequestHandler<GetOrdersForCurrentUser, IEnumerable<Order>>
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IApplicationDbContext _context;

	public GetOrdersForCurrentUserHandler(ICurrentUserService currentUserService, IApplicationDbContext context)
    {
        _currentUserService = currentUserService;
        _context = context;
    }

	public async Task<IEnumerable<Order>> Handle(GetOrdersForCurrentUser request, CancellationToken cancellationToken)
	{
		return await _context.Orders
            .Where(o => o.User.Id == _currentUserService.UserId)
            .ToListAsync(cancellationToken);
	}
}
