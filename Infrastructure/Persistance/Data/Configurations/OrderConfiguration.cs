using Domain.Entities.Order_Entitie;

namespace Persistance.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.Address, a => a.WithOwner());
            builder.HasMany(o => o.OrderItems).WithOne();
            builder.Property(o => o.PaymentStatus)
                   .HasConversion
                   (
                       paymentStatus => paymentStatus.ToString(),
                       paymentStatus => Enum.Parse<OrderPaymentStatus>(paymentStatus)
                   );
            builder.HasOne(o => o.DeliveryMethod).WithMany()
                   .OnDelete(DeleteBehavior.SetNull);
            builder.Property(o => o.SubTotal)
                   .HasColumnType("decimal(18,2)");
        }
    }
}
