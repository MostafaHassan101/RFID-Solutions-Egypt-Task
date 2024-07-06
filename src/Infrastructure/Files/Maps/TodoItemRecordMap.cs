using System.Globalization;
using RFID_Task.Application.TodoLists.Queries.ExportTodos;
using CsvHelper.Configuration;

namespace RFID_Task.Infrastructure.Files.Maps;

public class TodoItemRecordMap : ClassMap<TodoItemRecord>
{
    public TodoItemRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.Done).Convert(c => c.Value.Done ? "Yes" : "No");
    }
}
