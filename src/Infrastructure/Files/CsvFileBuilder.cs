using System.Globalization;
using RFID_Task.Application.Common.Interfaces;
using RFID_Task.Application.TodoLists.Queries.ExportTodos;
using RFID_Task.Infrastructure.Files.Maps;
using CsvHelper;

namespace RFID_Task.Infrastructure.Files;

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
