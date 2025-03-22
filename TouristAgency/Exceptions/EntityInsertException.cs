namespace TouristAgency.Exceptions
{
    public class EntityInsertException<TEntity> : Exception
    {
        public EntityInsertException()
           : base($"An error occurred while inserting an {typeof(TEntity).Name}.")
        {
        }
    }
}
