namespace Joaoaalves.FastCQRS.Abstractions.Exceptions
{
    public class InvalidCommandException(string message, string details) : Exception(message)
    {
        public string Details { get; } = details;
    }
}