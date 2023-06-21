using Domain.Entity;
using Infarstructure.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstructure.Data
{
    public class ApplicationDbcontext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options):base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>().ToTable("User", "Identity");
            builder.Entity<IdentityRole>().ToTable("Role", "Identity");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole", "Identity");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", "Identity");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "Identity");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", "Identity");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken", "Identity");


            builder.Entity<Category>().Property(x => x.ID).HasDefaultValueSql("(newid())");
            builder.Entity<LogCategory>().Property(x => x.ID).HasDefaultValueSql("(newid())");
            builder.Entity<SubCategory>().Property(x => x.ID).HasDefaultValueSql("(newid())");
            builder.Entity<LogSubCategory>().Property(x => x.ID).HasDefaultValueSql("(newid())");
            builder.Entity<Book>().Property(x => x.ID).HasDefaultValueSql("(newid())");
            builder.Entity<LogBook>().Property(x => x.ID).HasDefaultValueSql("(newid())");
            builder.Entity<VwUser>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwUsers");

            });
        }
           
        public DbSet<Category> categories { get; set; }
        public DbSet<LogCategory> logCategories { get; set; }
        public DbSet<SubCategory> subCategories { get; set; }
        public DbSet<LogSubCategory> logSubCategories { get; set; }
        public DbSet<Book> books { get; set; }
        public DbSet<LogBook> logBooks{ get; set; }
        public DbSet<VwUser> VwUsers { get; set; }

    }
}
