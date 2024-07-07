using FluentValidation;

namespace RFID.SimpleTask.Application.Orders.Commands.Delete;

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty();
    }
}