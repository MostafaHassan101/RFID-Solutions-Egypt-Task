using RFID_Task.Application.Common.Interfaces;

namespace RFID_Task.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
