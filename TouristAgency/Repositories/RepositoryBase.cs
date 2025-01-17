using Microsoft.EntityFrameworkCore;
using TouristAgency.Data;

namespace TouristAgency.Repositories
{
    public class RepositoryBase<TEntity> where TEntity : class
    {
        protected readonly DbSet<TEntity> _table;
        protected DataContext _dataContext { get; set; }

        public RepositoryBase(DataContext dataContext)
        {
            _table = dataContext.Set<TEntity>();
            _dataContext = dataContext;
        }
    }
}
