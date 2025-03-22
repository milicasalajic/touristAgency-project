namespace TouristAgency.Exceptions
{

    public class PasswordsNotMatchingException : Exception
    {
        public PasswordsNotMatchingException() : base($"Repeat password must be same as new password.")
        {
        }
    }
}
