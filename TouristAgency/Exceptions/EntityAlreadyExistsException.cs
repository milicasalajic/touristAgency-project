namespace TouristAgency.Exceptions
{
    public class EntityAlreadyExistsException<TEntity> : Exception
    {
        public EntityAlreadyExistsException(string identificator) : base($"{typeof(TEntity).Name} with that {identificator} already exists.")
        {
        }
    }
}
