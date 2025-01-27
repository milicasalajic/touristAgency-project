namespace TouristAgency.Exceptions
{
    public class SingleEntityRetrievalException<TEntity> : Exception
    {
        public SingleEntityRetrievalException()
           : base($"An error occurred while retrieving {typeof(TEntity).Name} by id.")
        {
        }
    }
}
