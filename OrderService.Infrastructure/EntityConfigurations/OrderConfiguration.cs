using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;

namespace OrderService.Infrastructure.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(order => order.Id);

        builder.Property(order => order.BookId)
            .IsRequired();

        builder.Property(order => order.Quantity)
            .IsRequired();

        builder.Property(order => order.Status)
            .IsRequired()
            .HasDefaultValue(OrderStatus.Pending);
    }
}
