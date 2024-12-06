using OpenApiProject1.MiddleWareExceptionHandeling;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {
    }
}
