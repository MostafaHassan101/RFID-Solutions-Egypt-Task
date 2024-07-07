using Microsoft.AspNetCore.Identity;
using RFID.SimpleTask.Domain.Entities;

namespace RFID.SimpleTask.Infrastructure.Identity;

public class ApplicationUser : IdentityUser, IApplicationUser
{
    public string? Name { get; set; }

    public bool IsActive { get; set; } = true;

    public IEnumerable<Order>? Orders { get; set; }
}