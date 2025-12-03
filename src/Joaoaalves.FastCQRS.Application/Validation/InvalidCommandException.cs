namespace Joaoaalves.FastCQRS.Application.Validation
{
    public class InvalidCommandException(string message, string details) : Exception(message)
    {
        public string Details { get; } = details;
    }
}