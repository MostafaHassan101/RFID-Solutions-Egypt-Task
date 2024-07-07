using RFID.SimpleTask.Application.Common.Mappings;
using RFID.SimpleTask.Domain.Entities;

namespace RFID.SimpleTask.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public class TodoItemBriefDto : IMapFrom<TodoItem>
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
}
