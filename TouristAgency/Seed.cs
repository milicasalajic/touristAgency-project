using System.Diagnostics.Metrics;
using TouristAgency.Data;

namespace TouristAgency
{
    public class Seed
    {
        // unapred popunjava Database podacima, treba popuniti podacima kao u springboot sql bazu

        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
           
        }
    }
}
