using FluentValidation;

namespace RFID.SimpleTask.Application.Orders.Queries.Search;

public class SearchOrderQueryValidator : AbstractValidator<SearchOrderQuery>
{
    public SearchOrderQueryValidator()
    {
        RuleFor(v => v.SearchTerm)
            .NotNull().NotEmpty().WithMessage("Search term is required");
    }
}