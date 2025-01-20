namespace TouristAgency.Exceptions
{
    public class EntityNotFoundException<TEntity> : Exception
    {
        public EntityNotFoundException() : base($"{typeof(TEntity).Name} with that id does not exist.")
        {
        }
    }
}
