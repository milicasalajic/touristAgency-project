namespace TouristAgency.Exceptions
{
    public class IncorrectPasswordException : Exception
    {
        public IncorrectPasswordException() : base("Wrong old password.")
        {
        }
    }
}
