namespace TouristAgency.Exceptions
{
    public class InvalidUserIdException : Exception
    {
        public InvalidUserIdException() : base($"Invalid user ID.")
        {
        }
    }
}
