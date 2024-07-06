using RFID_Task.Application.TodoLists.Queries.ExportTodos;

namespace RFID_Task.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
