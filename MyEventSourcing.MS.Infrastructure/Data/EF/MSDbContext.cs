using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyEventSourcing.Common.Infrastructure.Data.EF;
using MyEventSourcing.MS.Domain.ReadModel.Cart;
using MyEventSourcing.MS.Domain.ReadModel.Customer;
using MyEventSourcing.MS.Domain.ReadModel.Product;

namespace MyEventSourcing.MS.Infrastructure.Data.EF;

public class MSDbContext : BaseDbContext
{
    public DbSet<CartRM> CartRMs => Set<CartRM>();
    public DbSet<CartItemRM> CartItemRMs => Set<CartItemRM>();
    public DbSet<CustomerRM> CustomerRMs => Set<CustomerRM>();
    public DbSet<ProductRM> ProductRMs => Set<ProductRM>();

    public MSDbContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CartRM>(ConfigureCartRMs);
        modelBuilder.Entity<CartItemRM>(ConfigureCartItemRMs);
        modelBuilder.Entity<CustomerRM>(ConfigureCustomerRMs);
        modelBuilder.Entity<ProductRM>(ConfigureProductRMs);
    }

    private void ConfigureCartRMs(EntityTypeBuilder<CartRM> builder)
    {
        builder.ToTable("Carts");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.TotalItems).IsRequired();
        builder.Property(a => a.CustomerName).IsRequired();

        builder.HasOne<CustomerRM>().WithMany().HasForeignKey(a => a.CustomerId);
        builder.HasMany(a => a.Items).WithOne().HasForeignKey(a => a.CartId);
    }

    private void ConfigureCartItemRMs(EntityTypeBuilder<CartItemRM> builder)
    {
        builder.ToTable("CartItems");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Quantity).IsRequired();
        builder.Property(a => a.ProductName).IsRequired();

        builder.HasOne<ProductRM>().WithMany().HasForeignKey(a => a.ProductId);
    }

    private void ConfigureCustomerRMs(EntityTypeBuilder<CustomerRM> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).IsRequired();
    }

    private void ConfigureProductRMs(EntityTypeBuilder<ProductRM> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).IsRequired();
    }
}