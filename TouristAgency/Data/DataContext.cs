using Microsoft.EntityFrameworkCore;
using TouristAgency.Model;

namespace TouristAgency.Data
{
    public class DataContext : DbContext
    {   // fajl za pisanje tabela u bazi
                                                                    // gura podatke u DbContext
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfiguracija veze između TouristPackage i Organizer
            modelBuilder.Entity<TouristPackage>()
                .HasOne(tp => tp.Organizer)
                .WithMany(o => o.touristPackages)
        
                .OnDelete(DeleteBehavior.NoAction); // Sprečava kaskadno brisanje

          
            base.OnModelCreating(modelBuilder);
        }

        // navodi sve tabele koje zelis
        public DbSet<User> Users { get; set; }
        public DbSet<TouristPackage> TouristPackages { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Tourist> Tourists { get; set; }    
        public DbSet<Category> Categories { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

       

    }
}
