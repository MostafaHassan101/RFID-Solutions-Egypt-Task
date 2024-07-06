using RFID_Task.Application.Common.Mappings;
using RFID_Task.Domain.Entities;

namespace RFID_Task.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; init; }

    public bool Done { get; init; }
}
