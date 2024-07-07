using RFID.SimpleTask.Application.Common.Mappings;
using RFID.SimpleTask.Domain.Entities;

namespace RFID.SimpleTask.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; init; }

    public bool Done { get; init; }
}
