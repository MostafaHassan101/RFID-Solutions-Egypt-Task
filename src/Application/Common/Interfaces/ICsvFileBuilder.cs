using RFID.SimpleTask.Application.TodoLists.Queries.ExportTodos;

namespace RFID.SimpleTask.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
