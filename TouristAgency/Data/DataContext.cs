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
