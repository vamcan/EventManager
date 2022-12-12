namespace EventManager.Core.Domain.Base.Exceptions
{
    public class InvalidDataAccessException : DomainStateException
    {
        public InvalidDataAccessException(string message, params string[] parameters) : base(message, parameters)
        {
            Parameters = parameters;
        }
    }
}
