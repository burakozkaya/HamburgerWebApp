using HamburgerWebApp.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HamburgerWebApp.DAL.Context;

public class AppDbContext : IdentityDbContext<AppUser, IdentityRole, string>
{
    public DbSet<Menu> Menus { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderSize> OrderSizes { get; set; }
    public DbSet<Extra> Extras { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Order>()
            .HasMany(o => o.Extras)
            .WithMany(e => e.Orders)
            .UsingEntity(j => j.ToTable("OrderExtras"));


        var hasher = new PasswordHasher<AppUser>();
        //seed data for orderSize
        modelBuilder.Entity<OrderSize>().HasData(new OrderSize { Id = 1, Size = "Small", PriceMultiplier = 1 });
        modelBuilder.Entity<OrderSize>().HasData(new OrderSize { Id = 2, Size = "Medium", PriceMultiplier = 1.4m });
        modelBuilder.Entity<OrderSize>().HasData(new OrderSize { Id = 3, Size = "Large", PriceMultiplier = 1.8m });
        //seed data for admin user
        var adminPw = hasher.HashPassword(null, "admin");
        modelBuilder.Entity<AppUser>().HasData(new AppUser
        {
            Id = "1",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            PasswordHash = adminPw
        });
        //seed data for identity role
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "1",
            Name = "Admin",
            NormalizedName = "ADMIN"
        });
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "2",
            Name = "User",
            NormalizedName = "USER"
        });
        //seed data for identity user role
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = "1",
            UserId = "1"
        });
        base.OnModelCreating(modelBuilder);
    }
}