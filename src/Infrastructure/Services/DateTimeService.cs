using RFID.SimpleTask.Application.Common.Interfaces;

namespace RFID.SimpleTask.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
