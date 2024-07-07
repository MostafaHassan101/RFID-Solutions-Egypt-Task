namespace RFID.SimpleTask.Domain.Entities;

public class OrderItem : BaseEntity
{
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;

}