using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Users.Models;

namespace Users.Data;

public class UserDbContext : IdentityDbContext<User>
{
    public DbSet<IdentityRole> Roles {  get; set; }
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>().Ignore(U => U.PhoneNumberConfirmed);
        builder.Entity<User>().Ignore(U => U.PhoneNumber);
        builder.Entity<User>().Ignore(U => U.AccessFailedCount);
        builder.Entity<User>().Ignore(U => U.LockoutEnd);
        builder.Entity<User>().Ignore(U => U.LockoutEnabled);
        builder.Entity<User>().Ignore(U => U.TwoFactorEnabled);
        builder.Entity<User>().HasIndex(U => U.DNI).IsUnique();
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "User",
                NormalizedName = "USER"
            },
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Responsible",
                NormalizedName = "RESPONSIBLE"
            }
       );


    }
}

