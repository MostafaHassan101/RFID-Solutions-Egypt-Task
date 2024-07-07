namespace RFID.SimpleTask.Domain.Entities;

public class Order : BaseAuditableEntity
{
    public string UserId { get; set; } = null!;
    public IApplicationUser User { get; set; } = null!;
    public IEnumerable<OrderItem> OrderItems { get; set; } = null!;
    public bool IsDeleted { get; set; } = false;
}
