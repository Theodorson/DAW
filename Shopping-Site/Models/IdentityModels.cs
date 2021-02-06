using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Shopping_Site.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
           
        }



        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Phone> Phones { get; set; }
        public DbSet<Processor> Processors { get; set; }
        public DbSet<Camera> Cameras { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Battery> Batteries { get; set; }
        public DbSet<CartOrderCompletion> CartOrders { get; set; }
        public DbSet<Birth> Births { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Phone>().HasMany(p => p.Cameras).WithMany(v => v.Phones);
            modelBuilder.Entity<Camera>().HasMany(p => p.Features).WithMany(v => v.Cameras);
        }

        internal object Database()
        {
            throw new NotImplementedException();
        }
    }
}