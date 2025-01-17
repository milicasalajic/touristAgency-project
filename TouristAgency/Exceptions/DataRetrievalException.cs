namespace TouristAgency.Exceptions
{
    public class DataRetrievalException<TEntity> : Exception
    {
        public DataRetrievalException() : base($"An error occurred while retrieving all {typeof(TEntity).Name}.")
        {
        }
    }
}
