namespace Taskill.Exceptions;

public class DomainException : Exception
{
    public int StatusCode { get; set; }

    public DomainException(string message, int statusCode = 400) : base(message)
    {
        StatusCode = statusCode;
    }
}
