namespace Persistance.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Price).HasColumnType("decimal(18,3)");
            builder.HasOne(P => P.ProductBrand)
                   .WithMany()
                   .HasForeignKey(P => P.BrandId);
            builder.HasOne(P => P.ProductType)
                   .WithMany()
                   .HasForeignKey(P => P.TypeId);
        }
    }
}
