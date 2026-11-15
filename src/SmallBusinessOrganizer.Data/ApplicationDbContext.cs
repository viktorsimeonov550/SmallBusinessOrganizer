using Microsoft.EntityFrameworkCore;
using SmallBusinessOrganizer.Data.Entities;

namespace SmallBusinessOrganizer.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<StockLevel> StockLevels => Set<StockLevel>();
    public DbSet<StockMovement> StockMovements => Set<StockMovement>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
    public DbSet<PurchaseOrderItem> PurchaseOrderItems => Set<PurchaseOrderItem>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<SalesOrder> SalesOrders => Set<SalesOrder>();
    public DbSet<SalesOrderItem> SalesOrderItems => Set<SalesOrderItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        builder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
        builder.Entity<Product>().HasIndex(p => p.Sku).IsUnique();
        builder.Entity<Product>().Property(p => p.UnitPrice).HasPrecision(18, 2);
        builder.Entity<Product>().ToTable(t => t.HasCheckConstraint("CK_Product_MinStock", "[MinStockLevel] >= 0"));
        builder.Entity<Location>().HasIndex(l => l.Code).IsUnique();

        builder.Entity<StockLevel>().HasKey(s => new { s.ProductId, s.LocationId });
        builder.Entity<StockLevel>().ToTable(t => t.HasCheckConstraint("CK_StockLevel_Quantity", "[Quantity] >= 0"));

        builder.Entity<StockMovement>().ToTable(t => t.HasCheckConstraint("CK_StockMovement_Quantity", "[Quantity] > 0"));
        builder.Entity<StockMovement>()
            .HasOne(m => m.FromLocation).WithMany().HasForeignKey(m => m.FromLocationId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<StockMovement>()
            .HasOne(m => m.ToLocation).WithMany().HasForeignKey(m => m.ToLocationId).OnDelete(DeleteBehavior.Restrict);

        builder.Entity<PurchaseOrderItem>().Property(i => i.UnitCost).HasPrecision(18, 2);
        builder.Entity<SalesOrderItem>().Property(i => i.UnitPrice).HasPrecision(18, 2);

        // Avoid SQL Server multiple cascade path errors: restrict all foreign keys by default.
        foreach (var fk in builder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()))
        {
            if (fk.DeleteBehavior == DeleteBehavior.Cascade)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
