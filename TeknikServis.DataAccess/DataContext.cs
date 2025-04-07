using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeknikServis.DataAccess.Configuration;
using TeknikServis.Entities;
using TeknikServis.Entities.Servis;

namespace TeknikServis.DataAccess
{


    public class TeknikServisDbContext : IdentityDbContext<Kullanici, Roles, string>
    {
        public TeknikServisDbContext(DbContextOptions<TeknikServisDbContext> options) : base(options)
        {
        }

        public DbSet<Musteri> Musteris { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Roles> Roller { get; set; }

        public DbSet<Marka> Markalar { get; set; }
        public DbSet<Model> Modeller { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            //builder.ApplyConfiguration(new IslemConfiguration());

            builder.ApplyConfigurationsFromAssembly(typeof(TeknikServisDbContext).Assembly);
            base.OnModelCreating(builder);
        }

    }


 

}

// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore;
// using TechService.Entities.Service;

// namespace TechService.DataAccess
// {
//     public class TechServiceDbContext(DbContextOptions<TechServiceDbContext> options) 
//         : IdentityDbContext<User, Role, int>(options)
//     {
//         public DbSet<Customer> Customers { get; set; }
//         public DbSet<User> Users { get; set; }
//         public DbSet<Role> Roles { get; set; }

//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             optionsBuilder
//                 .UseSqlServer("YourConnectionString", options =>
//                     options.MigrationsAssembly("TechService.DataAccess")); // Migration Assembly is specified here
//         }
//     }
// }