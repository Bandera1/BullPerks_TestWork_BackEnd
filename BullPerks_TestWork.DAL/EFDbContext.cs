using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BullPerks_TestWork.Domain.DB.Models;
using BullPerks_TestWork.Domain.DB.IdentityModels;
using BullPerks_TestWork.Domain.Constants;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection.Emit;

namespace BullPerks_TestWork.DAL
{
    public class EFDbContext : IdentityDbContext<DbUser, IdentityRole, string, IdentityUserClaim<string>,
    IdentityUserRole<string>, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<DbToken> Tokens { get; set; }

        protected override async void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Roles - 1
            builder.Entity<IdentityRole>().HasData(new IdentityRole {
                Id = Guid.NewGuid().ToString(),
                Name = ProjectRoles.ADMIN, 
                NormalizedName = ProjectRoles.ADMIN.ToUpper() 
            });

        }
    }
}
