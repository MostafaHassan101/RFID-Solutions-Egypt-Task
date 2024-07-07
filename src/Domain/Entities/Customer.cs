namespace RFID.SimpleTask.Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public IEnumerable<Order>? Orders { get; set; }
}
