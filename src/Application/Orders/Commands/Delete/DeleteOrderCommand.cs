using MediatR;
using Microsoft.EntityFrameworkCore;
using RFID.SimpleTask.Application.Common.Exceptions;
using RFID.SimpleTask.Application.Common.Interfaces;
using RFID.SimpleTask.Domain.Entities;

namespace RFID.SimpleTask.Application.Orders.Commands.Delete;

public record DeleteOrderCommand : IRequest
{
	public int Id { get; set; }
}

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
	private readonly IApplicationDbContext _context;

    public DeleteOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
	{
        var entity = await _context.Orders.FirstOrDefaultAsync( o => o.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Order), request.Id);
        }

        _context.Orders.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
	}
}
