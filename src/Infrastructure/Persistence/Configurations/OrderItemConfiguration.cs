using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RFID.SimpleTask.Domain.Entities;

namespace RFID.SimpleTask.Infrastructure.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(i => i.Id);
        
        builder.HasIndex(i => i.OrderId);
        builder.HasIndex( i=> i.ProductName);

        builder.HasOne(i => i.Order)
            .WithMany(i => i.OrderItems)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
