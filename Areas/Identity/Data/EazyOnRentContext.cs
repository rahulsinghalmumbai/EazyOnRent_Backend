using System.Reflection.Emit;
using EazyOnRent.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EazyOnRent.Data;

public class EazyOnRentContext : IdentityDbContext<IdentityUser>
{
    public EazyOnRentContext(DbContextOptions<EazyOnRentContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // builder.Entity<ItemImage>().ToTable("ItemImages", t => t.ExcludeFromMigrations());
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Lister> Listers { get; set; }
    public DbSet<Renter> Renters { get; set; }
    public DbSet<ListerItem> ListerItems { get; set; }
    public DbSet<RenterItem> RenterItems { get; set; }
    public DbSet<ItemImage> ItemImages { get; set; }
    public DbSet<Categorie> Categories { get; set; }
    public DbSet<dbViewed> dbVieweds { get; set; }
}
