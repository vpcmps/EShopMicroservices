using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configurations;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .HasConversion(orderId => orderId.Value, dbId => OrderId.Of(dbId));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId);

        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);

        builder.ComplexProperty(o => o.OrderName, nameBuilder =>
        {
            nameBuilder.Property(nameBuilder => nameBuilder.Value)
            .HasColumnName(nameof(Order.OrderName))
            .HasMaxLength(100)
            .IsRequired();
        });

        builder.ComplexProperty(o => o.ShippingAddress, addressBuilder =>
        {
            addressBuilder.Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            addressBuilder.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsRequired();

            addressBuilder.Property(x => x.EmailAddress)
                .HasMaxLength(255)
                .IsRequired();

            addressBuilder.Property(x => x.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

            addressBuilder.Property(x => x.Country)
                .HasMaxLength(50);

            addressBuilder.Property(x => x.State)
                .HasMaxLength(50);

            addressBuilder.Property(x => x.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.BillingAddress, addressBuilder =>
        {
            addressBuilder.Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            addressBuilder.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsRequired();

            addressBuilder.Property(x => x.EmailAddress)
                .HasMaxLength(255)
                .IsRequired();

            addressBuilder.Property(x => x.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

            addressBuilder.Property(x => x.Country)
                .HasMaxLength(50);

            addressBuilder.Property(x => x.State)
                .HasMaxLength(50);

            addressBuilder.Property(x => x.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.Payment, paymentBuilder =>
        {
            paymentBuilder.Property(x => x.CardName)
                .HasMaxLength(100)
                .IsRequired();
            paymentBuilder.Property(x => x.CardNumber)
                .HasMaxLength(24)
                .IsRequired();
            paymentBuilder.Property(x => x.Expiration)
                .HasMaxLength(10)
                .IsRequired();
            paymentBuilder.Property(x => x.CVV)
                .HasMaxLength(3)
                .IsRequired();
            paymentBuilder.Property(x => x.PaymentMethod);
        });

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(s => s.ToString(),
            dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));
    }
}
