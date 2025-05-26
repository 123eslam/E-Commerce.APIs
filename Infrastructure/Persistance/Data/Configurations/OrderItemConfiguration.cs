using Domain.Entities.Order_Entitie;

namespace Persistance.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(o => o.Price).HasColumnType("decimal(18,3)");
            builder.OwnsOne(o => o.Product, p => p.WithOwner());
        }
    }
}
