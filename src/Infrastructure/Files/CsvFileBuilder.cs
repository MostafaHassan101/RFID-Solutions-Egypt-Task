using System.Globalization;
using RFID.SimpleTask.Application.Common.Interfaces;
using RFID.SimpleTask.Application.TodoLists.Queries.ExportTodos;
using RFID.SimpleTask.Infrastructure.Files.Maps;
using CsvHelper;

namespace RFID.SimpleTask.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Context.RegisterClassMap<TodoItemRecordMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
