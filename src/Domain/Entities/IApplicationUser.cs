namespace RFID.SimpleTask.Domain.Entities;

public interface IApplicationUser
{
    string? Id { get; }
    string? Name { get; }
    string? UserName { get; }
    string? Email { get;}
    bool IsActive { get; }
    IEnumerable<Order>? Orders { get; }
}