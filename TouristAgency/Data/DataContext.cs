using Microsoft.EntityFrameworkCore;

namespace TouristAgency.Data
{
    public class DataContext : DbContext
    {   // fajl za pisanje tabela u bazi
                                                                    // gura podatke u DbContext
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        // navodi sve tabele koje zelis
    }
}
