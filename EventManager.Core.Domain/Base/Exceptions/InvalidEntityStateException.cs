namespace EventManager.Core.Domain.Base.Exceptions
{
    public class InvalidEntityStateException : DomainStateException
    {
        public InvalidEntityStateException(string message, params string[] parameters) : base(message)
        {
            Parameters = parameters;
        }
    }
}